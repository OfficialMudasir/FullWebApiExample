using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FullWebApiExample.Models
{
    public class MockRepository : IRepository
    {

        private Dictionary<int, Employee> items;
        public MockRepository()
        {
            items = new Dictionary<int, Employee>();
            new List<Employee>
            {
                new Employee {Id = 1, Name = "Mudasir", Email = "Kashmir", Mobile = "Everest", Address = "kasheer"},
                new Employee {Id = 2, Name = "Mussaib", Email = "delhi", Mobile = "New York", Address = "kasheer"},
                new Employee {Id = 3, Name = "Ahmad", Email = "Aligarh", Mobile = "USA", Address = "kasheer"}

            }.ForEach(r => AddEmployee(r));
        }

        // this is for [httpget("{id}")] to retturn perticular id item
        public Employee this[int id] => items.ContainsKey(id)? items[id]:null;
        // this is for [httpget] to return all items
        public IEnumerable<Employee> Employees => items.Values;
        // adding new record to the Reservation
        public Employee AddEmployee(Employee employee)
        {
            int key = items.Count();
            //Debug.Write(key);
            while (items.ContainsKey(key))
            {
                key++;
                employee.Id = key;
                
            }
            
            items[employee.Id] = employee;
            return employee;
        }

       

        public void DeleteEmployee(int id) => items.Remove(id);

        
       

        public Employee Details(Employee employee)
        {
            if(employee.Id != 0)
            {
                employee = items[employee.Id];
            }
            return employee;
        }

        public Employee UpdateEmployee(Employee employee) => AddEmployee(employee);


        
    }
}
