using LeaveBook.Data;
using LeaveBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeaveBook.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ILogger<AuthRepository> _logger;
        private readonly ApplicationDbContext _ctx;
        private readonly ISharedRepository _sharedRepo;
        public AuthRepository(ILogger<AuthRepository> logger, ApplicationDbContext context, ISharedRepository sharedRepository)
        {
            _logger = logger;
            _ctx = context;
            _sharedRepo = sharedRepository;
        }

        public async Task<Employee> Login(string userName, string password)
        {
            userName = userName.ToLower();
            var employee = await _ctx.Employees.SingleOrDefaultAsync(e => e.UserName == userName);
            if (employee == null)
            {
                _logger.LogWarning("no such employee exists...");
                return null;
            }
            var isPassword = IsPassword(password, employee.HashPassword, employee.Key);
            if (isPassword == false)
            {
                _logger.LogWarning("password is incorrect...");
                return null;
            }
            return employee;
        }

        public bool IsPassword(string password, byte[] hashPassword, byte[] passwordSalt)
        {
            using (var hashedPasswordFromDb = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var enteredPasswordHash = hashedPasswordFromDb.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < enteredPasswordHash.Length; i++)
                {
                    if (hashPassword[i] != enteredPasswordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<Employee> Register(Employee employee, string password)
        {
            employee.UserName = employee.UserName.ToLower();
            CreatePasswordWithEncryption(password, out byte[] passwordHash, out byte[] key);
            employee.HashPassword = passwordHash;
            employee.Key = key;
            employee.NormalizedUserName = employee.UserName.ToUpper().Normalize();
            employee.NormalizedEmail = employee.Email.ToUpper().Normalize();
            employee.PasswordHash = password;
            _sharedRepo.Add<Employee>(employee);
            
            if (await _sharedRepo.SaveAll())
            {
                _logger.LogInformation("employee registered successfully!");
                return employee;
            }
            else
                return null;
        }

        private void CreatePasswordWithEncryption(string password, out byte[] passwordHash, out byte[] key)
        {
            using (var hashedPassword = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hashedPassword.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                key = hashedPassword.Key;
            }
        }

        public List<byte[]> CreatePasswordWithEncryption(string password)
        {
            byte[] passwordHash, key;
            using (var hashedPassword = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hashedPassword.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                key = hashedPassword.Key;
            }
            List<byte[]> password_hash_key = new List<byte[]>();
            password_hash_key.Add(passwordHash);
            password_hash_key.Add(key);
            return password_hash_key;
        }
    }
}
