using LeaveBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeaveBook.Repositories
{
    public interface IAuthRepository
    {
        Task<Employee> Register(Employee employee, string password);
        Task<Employee> Login(string userName, string password);
        bool IsPassword(string password, byte[] passwordHash, byte[] passwordSalt);
        List<byte[]> CreatePasswordWithEncryption(string password);
    }
}