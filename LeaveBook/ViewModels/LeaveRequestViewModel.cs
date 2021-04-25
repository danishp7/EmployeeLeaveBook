using LeaveBook.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeaveBook.ViewModels
{
    public class LeaveRequestViewModel
    {
        [key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string ApprovedById { get; set; }
        public int LeaveTypeId { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        public DateTime AppliedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status LeaveStatus { get; set; }

        public List<LeaveTypesVM> LeaveTypes { get; set; }
    }
}
