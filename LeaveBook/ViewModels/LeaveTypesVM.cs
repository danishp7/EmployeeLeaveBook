using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.ViewModels
{
    public class LeaveTypesVM
    {
        [key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int AllottedDays { get; set; }
    }
}
