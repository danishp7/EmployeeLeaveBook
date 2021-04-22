using LeaveBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.Repositories
{
    public interface ILeaveTypeRepository
    {
        Task<List<LeaveType>> GetAllLeaveTypes();
    }
}
