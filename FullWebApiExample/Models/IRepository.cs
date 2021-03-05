using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullWebApiExample.Models
{
    public interface IRepository
    {
        IEnumerable<Employee> Employees { get; }
        Employee this[int id] { get; }
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
        Employee Details(Employee employee);
    }
}
    