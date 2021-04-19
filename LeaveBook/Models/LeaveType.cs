using System.Collections.Generic;

namespace LeaveBook.Models
{
    public class LeaveType
    {
        public int Id { get; set; }

        // relation with leave request (1 to *)
        public ICollection<LeaveRequest> LeaveRequests { get; set; }

        // relation with leave audit (1 to *)
        public ICollection<LeaveAudit> LeaveAudits { get; set; }

        public string Name { get; set; }
        public int AllottedDays { get; set; }
    }
}
