using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Services.InterfaceServie
{
    public interface IPayrollService
    {
        List<Payroll> GetPayrollByEmp(int employeeId);
        List<Payroll> GetPayrollByMonth(int employeeId, int month, int year);
        int GetTotalWorkingHours(int employeeId, int month, int year);
        int GetTotalOutTime(int employeeId, int month, int year);
        int GetTotalSalary(int employeeId, int month, int year);
        void AddPayroll(Payroll payroll);
        void UpdatePayroll(Payroll payroll);
        Payroll GetPayrollByMonthAndYear(int employeeId, int month, int year);
        Payroll GetPayrollByID(int id);
        Payroll GetById(int id);
		List<Payroll> GetAll();
    }
}
