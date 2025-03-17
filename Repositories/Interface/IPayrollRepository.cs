using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IPayrollRepository 
    {
        List<Payroll> GetPayrollByEmp(int employeeId);
        List<Payroll> GetPayrollByMonth(int employeeId, int month, int year);
        int GetTotalWorkingHours(int employeeId, int month, int year);
        int GetTotalOutTime(int employeeId, int month, int year);
        int GetTotalSalary(int employeeId, int month, int year);
        void AddPayroll(Payroll payroll);
        Payroll GetPayrollByMonthAndYear(int employeeId, int month, int year);
    
		Payroll GetById(int id);
		List<Payroll> GetAll();
	}
}
