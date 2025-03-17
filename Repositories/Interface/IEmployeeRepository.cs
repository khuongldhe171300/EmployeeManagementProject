using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        List<Employee> GetEmployees();
        void AddEmployee(Employee employee, string password);
        bool CheckEmailExisting(string email);
        void UpdateEmployee(Employee employee);
    }
}

