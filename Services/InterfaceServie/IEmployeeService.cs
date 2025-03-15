using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServie
{
	public interface IEmployeeService
	{
		List<DepartmentReport> GetEmployeeCountByDepartment();
		List<PositionReport> GetEmployeeCountByPosition();
		List<GenderReport> GetEmployeeCountByGender();
	}
}
