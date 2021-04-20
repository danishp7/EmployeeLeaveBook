using LeaveBook.Enums;
using System;

namespace LeaveBook.ViewModels
{
    public class LeaveRequestViewModel
    {
        [key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string ApprovedById { get; set; }
        public int LeaveTypeId { get; set; }
        public string Reason { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status LeaveStatus { get; set; }
    }
}
