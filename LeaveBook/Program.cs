using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveBook.Data;
using LeaveBook.Helpers;
using LeaveBook.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LeaveBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            // for seeding data
            //var host = CreateHostBuilder(args);
            //SeedDb(host);
            //host.Run();
        }
        public static void SeedDb(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            var _logger = host.Services.GetService<ILogger<Program>>();
            using (var scope = scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // get the required parameters for the method
                    var _ctx = services.GetRequiredService<ApplicationDbContext>();

                    // get user manager
                    var _userManager = services.GetRequiredService<UserManager<Employee>>();

                    // add the migrations
                    _ctx.Database.Migrate();

                    // now get the seeder from transient service
                    var seed = services.GetRequiredService<SeedData>();
                    //seed.SeedLeaveTypeData();
                    seed.SeedEmployeeAsync().Wait();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to insert data into db...", null);
                }
            };

        }
        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        // for seeding data
        //public static IWebHost CreateHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .Build();
    }
}
