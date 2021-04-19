using LeaveBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.Data
{
    public interface ILeaveRequestRepository
    {
        Task<bool> ApplyForLeave(string employeeId, int leaveTypeId, string reason, DateTime appliedDate, DateTime endDate, DateTime startDay, DateTime endDay, Status leaveStatus, string approvedById);
    }
}
