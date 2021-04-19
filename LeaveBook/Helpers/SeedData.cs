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
        public SeedData(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _ctx = context;
            _userManager = userManager;
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
                user = new Employee()
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

                };

                // now we need to create this new user
                // 2nd argument is the password for the user
                var newUser = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (newUser == null)
                {
                    throw new InvalidOperationException("cannot create new user in seeding...!");
                }
            }
        }
    }
}
