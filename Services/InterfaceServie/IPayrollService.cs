using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServie
{
	public interface IPayrollService
	{
		Payroll GetById(int id);
		List<Payroll> GetAll();
	}
}
