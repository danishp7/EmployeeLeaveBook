using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.ViewModels
{
    public class EmployeeViewModel    
    {
        public byte[] HashPassword { get; set; }
        public byte[] Key { get; set; }
    }
}
