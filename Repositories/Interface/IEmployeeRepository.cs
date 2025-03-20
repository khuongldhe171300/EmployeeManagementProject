using BusinessObjects.Models;
using DataAssetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee GetEmployeeByID(int id);
        void UpdateEmployeeById(Employee employee);
        void UpdateEmployee(Employee employee);
        User GetUserByEmpID(int empID);
		List<DepartmentReport> GetEmployeeCountByDepartment();
		List<PositionReport> GetEmployeeCountByPosition();
		List<GenderReport> GetEmployeeCountByGender();
        List<Employee> GetEmployees();
        void AddEmployee(Employee employee, string password);
        bool CheckEmailExisting(string email);

        void DeleteEmployee(int employeeId);
	}
}

