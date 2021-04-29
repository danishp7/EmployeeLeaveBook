using LeaveBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<List<Employee>> GetAllEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
