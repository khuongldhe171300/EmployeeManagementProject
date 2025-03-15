using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Repositories.Repository;
using Services.InterfaceServie;

namespace Services.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeeService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public Employee GetEmployeeByID(int id) => employeeRepository.GetEmployeeByID(id);
        public void UpdateEmployee(Employee employee) => employeeRepository.UpdateEmployee(employee);
        public User GetUserByEmpID(int empID) => employeeRepository.GetUserByEmpID(empID);
    }
}
