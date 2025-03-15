using BusinessObjects.Models;
using DataAccessLayer;
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
        void UpdateEmployee(Employee employee);
        User GetUserByEmpID(int empID);
		List<DepartmentReport> GetEmployeeCountByDepartment();
		List<PositionReport> GetEmployeeCountByPosition();
		List<GenderReport> GetEmployeeCountByGender();
	}
}
