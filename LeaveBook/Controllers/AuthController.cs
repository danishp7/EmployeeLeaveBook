using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using LeaveBook.Models;
using LeaveBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LeaveBook.Repositories;
using LeaveBook.Enums;
using LeaveBook.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LeaveBook.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IAuthRepository _authRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<Employee> _employeeManager;
        private readonly SignInManager<Employee> _signInUser;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(ApplicationDbContext context, IAuthRepository authRepository, IMapper mapper, IConfiguration configuration,
                              UserManager<Employee> employeeManager, SignInManager<Employee> signInManager,
                              RoleManager<IdentityRole> roleManager, ILogger<AuthController> logger)
        {
            _ctx = context;
            _authRepo = authRepository;
            _mapper = mapper;
            _config = configuration;
            _employeeManager = employeeManager;
            _signInUser = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public IActionResult Register()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            return View(model);
        }

        public IActionResult Login()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            return View(model);
        }

        // register method
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(EmployeeViewModel employeeViewModel)
        {
            // checking model state
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("model state is invalid...");
                return RedirectToAction("Register", "Auth");
            }

            // checking if user with same email already exist
            var isEmployeeInDb = await _employeeManager.FindByEmailAsync(employeeViewModel.Email);
            if (isEmployeeInDb != null)
            {
                _logger.LogWarning("User exist with this email address. kindly provide different email id.");
                return RedirectToAction("Register", "Auth");
            }

            // set password
            var password_hash_key = _authRepo.CreatePasswordWithEncryption(employeeViewModel.Password);
            Employee employee = new Employee
            {
                UserName = employeeViewModel.Email,
                Email = employeeViewModel.Email,
                NormalizedUserName = employeeViewModel.Email.ToUpper().Normalize(),
                NormalizedEmail = employeeViewModel.Email.ToUpper().Normalize(),
                HashPassword = password_hash_key[0],
                Key = password_hash_key[1]
            };
            
            var isEmployeeRegistered = await _employeeManager.CreateAsync(employee, employeeViewModel.Password);
            if (!isEmployeeRegistered.Succeeded)
            {
                _logger.LogWarning("something went wrong...");
                return RedirectToAction("Register", "Auth");
            }

            // get all roles
            var roles = await _roleManager.Roles.ToListAsync();
            var role = roles.Where(rn => rn.Name == EmployerRole.Staff.ToString()).FirstOrDefault();
            var isRole = await _employeeManager.AddToRoleAsync(employee, role.Name);
            if (!isRole.Succeeded)
            { 
                _logger.LogWarning("something went wrong...");
                await _employeeManager.DeleteAsync(employee);
                return RedirectToAction("Register", "Auth");
            }
            return RedirectToAction("Login", "Auth");
        }

        // login method
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(EmployeeViewModel employeeViewModel)
        {
            // checking model state
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("model state is invalid...");
                return RedirectToAction("Login", "Auth");
            }

            // checking if employee exist or not
            var employee = await _employeeManager.FindByEmailAsync(employeeViewModel.Email);
            if (employee == null)
            {
                _logger.LogWarning("no such employee exists...");
                return RedirectToAction("Login", "Auth");
            }

            // checking if password is correct or not from identity framework
            var isPassword = await _employeeManager.CheckPasswordAsync(employee, employeeViewModel.Password);
            if (!isPassword)
            {
                _logger.LogWarning("incorrect password...");
                return RedirectToAction("Login", "Auth");
            }

            // checking if password is correct or not from auth repository
            var password = _authRepo.IsPassword(employeeViewModel.Password, employee.HashPassword, employee.Key);
            if (!isPassword)
            {
                _logger.LogWarning("incorrect password...");
                return RedirectToAction("Login", "Auth");
            }

            // checking if employee successfully login with credentials
            var isLogin = await _signInUser.PasswordSignInAsync(employee.Email, employeeViewModel.Password, false, false);
            if (isLogin.Succeeded)
            {
                IList<string> roles = await _employeeManager.GetRolesAsync(employee);
                
                // for the time being the no roles assigned to employees restricted to 1 
                // need to re configure this.
                var employerRole = roles[0];
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, employee.Id),
                    new Claim(ClaimTypes.Name, employee.UserName),
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.Role, employerRole)
                    // need to pass the unique role as well
                };

                var key = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(2000),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);
                string employeeToken = tokenHandler.WriteToken(token);
                return RedirectToAction("Index", "Home", new { logintoken = employeeToken });
                
            }
            _logger.LogWarning("login failed... incorrect email or password");
            return RedirectToAction("Login", "Auth");
            
        }

        // logout
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInUser.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}