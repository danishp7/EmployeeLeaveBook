namespace LeaveBook.ViewModels
{
    public class LeaveAuditViewModel
    {
        public string EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public double AppliedLeaves { get; set; }
        public double ApprovedLeaves { get; set; }
        public double DeclinedLeaves { get; set; }
        public double RemainingLeaves { get; set; }
    }
}
