using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssetObjects
{
	public class PayrollDAO
	{
		private readonly HrmanagementContext _context;
		
		public PayrollDAO(HrmanagementContext context)
		{
			_context = context;
		}

		public List<Payroll> GetAll()
		{
			return _context.Payrolls.ToList();
		}

		public Payroll GetById(int id)
		{
			return _context.Payrolls.FirstOrDefault(e => e.EmployeeId == id);
		}
	}
}
