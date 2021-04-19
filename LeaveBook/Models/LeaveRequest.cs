using LeaveBook.Enums;
using System;

namespace LeaveBook.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        // relation with employee (1 to *)
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        // relation with employee as admin (1 to *)
        public string ApprovedById { get; set; }
        public Employee ApprovedBy { get; set; }

        // relation with leave type (1 to *)
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }

        public string Reason { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public Status LeaveStatus { get; set; }
    }
}
