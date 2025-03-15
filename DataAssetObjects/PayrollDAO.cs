using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAssetObjects
{
    public class PayrollDAO
    {
        private readonly HrmanagementContext context;
        private readonly EmployeeDAO employeeDAO;
        public PayrollDAO(HrmanagementContext context)
        {
            this.context = context;
            employeeDAO = new EmployeeDAO(context);
        }
        public List<Payroll> GetPayrollByEmp(int employeeId)
        {
            return context.Payrolls.Where(p => p.EmployeeId == employeeId).ToList();
        }
        public List<Payroll> GetPayrollByMonth(int employeeId, int month, int year)
        {
            return context.Payrolls.Where(p => p.EmployeeId == employeeId && p.PaymentDate.Value.Month == month && p.PaymentDate.Value.Year == year).ToList();
        }
        public int GetTotalWorkingHours(int employeeId, int month, int year)
        {
            return context.Attendances
                .Where(a => a.EmployeeId == employeeId && a.WorkDate.Month == month && a.WorkDate.Year == year)
                .Sum(a => a.WorkHours.HasValue ? (int)a.WorkHours.Value : 0);
        }
        public int GetTotalOutTime(int employeeId, int month, int year)
        {
            int WorkHours = GetTotalWorkingHours(employeeId, month, year);
            int OT = WorkHours - 160;
            if (OT > 0)
            {
                return OT;
            }
            else
            {
                return 0;
            }
        }
        private int GetBasicSalary(Employee employee)
        {
            return employee.Position.BasicSalary;
        }
        public int GetTotalSalary(int employeeId, int month, int year)
        {
            int WorkHours = GetTotalWorkingHours(employeeId, month, year);
            int StandardHours = 0;
            if(WorkHours > 160)
            {
                StandardHours = 160;
            }
            else
            {
                StandardHours = WorkHours;
            }
            int OT = GetTotalOutTime(employeeId, month, year);
            var employee = employeeDAO.GetEmployeeByID(employeeId);
            int BasicSalary = GetBasicSalary(employee);
            int Insurance = (int)(BasicSalary * 10.5/100);
            int Tax = (int)(BasicSalary * 5 / 100);
            return BasicSalary * StandardHours/160 + OT * 50000 + 500000 - Insurance - Tax;
        }
    }
}
