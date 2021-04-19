namespace LeaveBook.Models
{
    public class LeaveAudit
    {
        public int Id { get; set; }
        // relation with employee (1 to *)
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        // relation with leave type (1 to *)
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }

        public double AppliedLeaves { get; set; }
        public double ApprovedLeaves { get; set; }
        public double DeclinedLeaves { get; set; }
        public double RemainingLeaves { get; set; }
    }
}
