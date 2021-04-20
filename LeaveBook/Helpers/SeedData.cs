using LeaveBook.Data;
using LeaveBook.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.Helpers
{
    public class SeedData
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(ApplicationDbContext context, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void SeedLeaveTypeData()
        {
            List<LeaveType> leaveTypes = null;
            if (!_ctx.LeaveTypes.Any())
            {
                leaveTypes = new List<LeaveType>
                {
                    new LeaveType
                    {
                        Name = "Casual",
                        AllottedDays = 10
                    },
                    new LeaveType
                    {
                        Name = "Annual",
                        AllottedDays = 15
                    },
                    new LeaveType
                    {
                        Name = "Sick",
                        AllottedDays = 10
                    },
                    new LeaveType
                    {
                        Name = "Married",
                        AllottedDays = 6
                    },
                    new LeaveType
                    {
                        Name = "Forced",
                        AllottedDays = 4
                    }
                };
            }
            _ctx.AddRange(leaveTypes);
            _ctx.SaveChanges();
        }

        public async Task SeedEmployeeAsync()
        {
            _ctx.Database.EnsureCreated();

            // now we check the user
            var user = await _userManager.FindByEmailAsync("lionel.messi@treatdutch.com");
            if (user == null)
            {
                List<Employee> employees = new List<Employee>
                {
                    new Employee
                    {
                        // these two needs to be properly implemented in real scenerio...
                        HashPassword = null,
                        Key = null,

                        Email = "lionel.messi@leavebook.com",
                        // username is required field so we need to pass the value for it
                        // we use email as username for our app
                        UserName = "lionel.messi@leavebook.com",
                        NormalizedEmail = "LIONEL.MESSI@LEAVEBOOK.COM",
                        NormalizedUserName = "LIONEL.MESSI@LEAVEBOOK.COM"

                    },
                    new Employee
                    {
                        // these two needs to be properly implemented in real scenerio...
                        HashPassword = null,
                        Key = null,

                        Email = "neymar.junior@leavebook.com",
                        // username is required field so we need to pass the value for it
                        // we use email as username for our app
                        UserName = "neymar.junior@leavebook.com",
                        NormalizedEmail = "NEYMAR.JUNIOR@LEAVEBOOK.COM",
                        NormalizedUserName = "NEYMAR.JUNIOR@LEAVEBOOK.COM"
                    }

                };

                // creating roles
                var hrRole = new IdentityRole
                {
                    Name = "Hr",
                    NormalizedName = "HR"
                };
                var staffRole = new IdentityRole
                {
                    Name = "Staff",
                    NormalizedName = "STAFF"
                };
                await _roleManager.CreateAsync(hrRole);
                await _roleManager.CreateAsync(staffRole);

                // now we need to create this new user
                // 2nd argument is the password for the user
                var hr = await _userManager.CreateAsync(employees[0], "P@ssw0rd!");
                if (hr.Succeeded)
                {
                    await _userManager.AddToRoleAsync(employees[0], hrRole.Name);
                }

                var staff = await _userManager.CreateAsync(employees[1], "P@ssw0rd!");
                if (staff.Succeeded)
                {
                    await _userManager.AddToRoleAsync(employees[1], staffRole.Name);
                }

            }
        }
    }
}
