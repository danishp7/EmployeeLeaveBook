using LeaveBook.Data;
using LeaveBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.Repositories
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ILogger<LeaveTypeRepository> _logger;
        private readonly ApplicationDbContext _ctx;
        public LeaveTypeRepository(ILogger<LeaveTypeRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }
        public async Task<List<LeaveType>> GetAllLeaveTypes()
        {
            try
            {
                var leaveTypes = await _ctx.LeaveTypes.ToListAsync();
                if (!leaveTypes.Any())
                {
                    _logger.LogWarning("No Leave Type exist in our system...");
                    return null;
                }
                return leaveTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
