using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly EmployeeDAO employeeDAO;

        public EmployeeRepository()
        {
            this.employeeDAO = new EmployeeDAO();
        }

        public EmployeeRepository(EmployeeDAO employeeDAO)
        {
            this.employeeDAO = employeeDAO;
        }

        public Task Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void AddEmployee(Employee employee, string password)
        {
            employeeDAO.AddEmployee(employee, password);
        }

        public bool CheckEmailExisting(string email)
        {
           return employeeDAO.EmailExisting(email);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
           return await employeeDAO.GetAll();   
        }

        public Task<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployees()
        {
            return employeeDAO.GetEmployees();
        }
		public List<DepartmentReport> GetEmployeeCountByDepartment() => employeeDAO.GetEmployeeCountByDepartment();

		public List<GenderReport> GetEmployeeCountByGender() => employeeDAO.GetEmployeeCountByGender();

		public List<PositionReport> GetEmployeeCountByPosition() => employeeDAO.GetEmployeeCountByPosition();

		public Task Update(Employee entity)
        {
            throw new NotImplementedException();
        }
        public Employee GetEmployeeByID(int id) => employeeDAO.GetEmployeeByID(id);
        public void UpdateEmployeeById(Employee employee)
        {
            employeeDAO.UpdateEmployeeById(employee);
        }
        public void UpdateEmployee(Employee employee) => employeeDAO.UpdateEmployee(employee);
        public User GetUserByEmpID(int empID) => employeeDAO.GetUserByEmpID(empID);

        public void DeleteEmployee(int employeeId)
        {
            employeeDAO.DeleteEmployee(employeeId);
        }
    }
}
