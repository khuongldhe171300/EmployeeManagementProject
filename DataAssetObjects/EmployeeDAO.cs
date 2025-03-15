using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAssetObjects;

namespace DataAccessLayer
{
	public class EmployeeDAO
	{
		private readonly HrmanagementContext _context;

		public EmployeeDAO(HrmanagementContext context)
		{
			_context = context;
		}

		public List<DepartmentReport> GetEmployeeCountByDepartment()
		{
			return _context.Employees
				.GroupBy(e => e.DepartmentId)
				.Select(g => new DepartmentReport
				{
					DepartmentName = _context.Departments
						.Where(d => d.DepartmentId == g.Key)
						.Select(d => d.DepartmentName)
						.FirstOrDefault() ?? "Không xác định",
					EmployeeCount = g.Count()
				})
				.ToList();
		}

		public List<PositionReport> GetEmployeeCountByPosition()
		{
			return _context.Employees
				.GroupBy(e => e.PositionId)
				.Select(g => new PositionReport
				{
					PositionName = _context.Positions
						.Where(p => p.PositionId == g.Key)
						.Select(p => p.PositionName)
						.FirstOrDefault() ?? "Không xác định",
					EmployeeCount = g.Count()
				})
				.ToList();
		}


		public List<GenderReport> GetEmployeeCountByGender()
		{
			return _context.Employees
				.GroupBy(e => e.Gender)
				.Select(g => new GenderReport
				{
					Gender = g.Key,
					EmployeeCount = g.Count()
				})
				.ToList();
		}
	}

	public class DepartmentReport
	{
		public string DepartmentName { get; set; }
		public int EmployeeCount { get; set; }
	}

	public class PositionReport
	{
		public string PositionName { get; set; }
		public int EmployeeCount { get; set; }
	}

	public class GenderReport
	{
		public string Gender { get; set; }
		public int EmployeeCount { get; set; }
	}

}