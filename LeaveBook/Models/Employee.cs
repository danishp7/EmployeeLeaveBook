using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.Models
{
    public class Employee : IdentityUser    
    {
        // relation with employee (1 to *)
        public ICollection<LeaveRequest> LeaveRequests  { get; set; }
        public ICollection<LeaveRequest> AdminLeaveRequests { get; set; }

        // relation with leave audit (1 to *)
        public ICollection<LeaveAudit> LeaveAudits { get; set; }

        public byte[] HashPassword { get; set; }
        public byte[] Key { get; set; }
    }
}
