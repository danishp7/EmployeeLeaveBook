using System.Collections.Generic;

namespace LeaveBook.ViewModels
{
    public class LeaveTypeViewModel
    {
        [key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int AllottedDays { get; set; }
    }
}
