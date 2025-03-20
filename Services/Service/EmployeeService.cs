using DataAssetObjects;
using Repositories.Repository;
using Services.InterfaceServie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

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
        public void UpdateEmployee(Employee employee) => employeeRepository.UpdateEmployeeById(employee);
        public User GetUserByEmpID(int empID) => employeeRepository.GetUserByEmpID(empID);
        public List<DepartmentReport> GetEmployeeCountByDepartment() => employeeRepository.GetEmployeeCountByDepartment();

		public List<GenderReport> GetEmployeeCountByGender() => employeeRepository.GetEmployeeCountByGender();

		public List<PositionReport> GetEmployeeCountByPosition() => employeeRepository.GetEmployeeCountByPosition();

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await employeeRepository.GetAll();
        }

        public List<Employee> GetEmployees()
        {
            return employeeRepository.GetEmployees();
        }
    }
}
