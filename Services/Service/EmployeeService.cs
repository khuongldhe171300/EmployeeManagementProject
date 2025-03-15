using DataAccessLayer;
using Repositories.Repository;
using Services.InterfaceServie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
	public class EmployeeService : IEmployeeService
	{
		private readonly EmployeeRepository repository;

		public EmployeeService(EmployeeRepository repository)
		{
			this.repository = repository;
		}

		public List<DepartmentReport> GetEmployeeCountByDepartment() => repository.GetEmployeeCountByDepartment();

		public List<GenderReport> GetEmployeeCountByGender() => repository.GetEmployeeCountByGender();

		public List<PositionReport> GetEmployeeCountByPosition() => repository.GetEmployeeCountByPosition();
	}
}
