using System;
using System.Collections.Generic;
using System.Text;
using LeaveBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeaveBook.ViewModels;

namespace LeaveBook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAudit> LeaveAudits { get; set; }
        
        // configure relations
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // employee and leave request (1 employee can apply many leaves)
            builder.Entity<LeaveRequest>()
                .HasOne(e => e.Employee)
                .WithMany(lr => lr.LeaveRequests)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);


            // employee as admin and leave request (1 employee can apply many leaves)
            builder.Entity<LeaveRequest>()
                .HasOne(e => e.ApprovedBy)
                .WithMany(lr => lr.AdminLeaveRequests)
                .HasForeignKey(e => e.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

            // leave request and leave type (1 leave type can be in many keave requests)
            builder.Entity<LeaveRequest>()
                .HasOne(lt => lt.LeaveType)
                .WithMany(lr => lr.LeaveRequests)
                .HasForeignKey(lt => lt.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // leave type and leave audit (1 leave type can be in many leave audits)
            builder.Entity<LeaveAudit>()
                .HasOne(lt => lt.LeaveType)
                .WithMany(la => la.LeaveAudits)
                .HasForeignKey(lt => lt.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // leave audit and employee (1 employee can have as many leave audits as number of leave types)
            builder.Entity<LeaveAudit>()
                .HasOne(e => e.Employee)
                .WithMany(la => la.LeaveAudits)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        
        // configure relations
        public DbSet<LeaveBook.ViewModels.LeaveRequestViewModel> LeaveRequestViewModel { get; set; }
    }
}
