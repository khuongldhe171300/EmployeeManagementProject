using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class PayrollRepository : IPayrollRepository
    {

        private readonly PayrollDAO payrollDAO;
        public PayrollRepository(PayrollDAO payrollDAO)
        {
            this.payrollDAO = payrollDAO;
        }
        public List<Payroll> GetPayrollByEmp(int employeeId) => payrollDAO.GetPayrollByEmp(employeeId);
        public List<Payroll> GetPayrollByMonth(int employeeId, int month, int year) => payrollDAO.GetPayrollByMonth(employeeId, month, year);
        public int GetTotalWorkingHours(int employeeId, int month, int year) => payrollDAO.GetTotalWorkingHours(employeeId, month, year);
        public int GetTotalOutTime(int employeeId, int month, int year) => payrollDAO.GetTotalOutTime(employeeId, month, year);
        public int GetTotalSalary(int employeeId, int month, int year) => payrollDAO.GetTotalSalary(employeeId, month, year);
		public List<Payroll> GetAll() => payrollDAO.GetAll();
		public Payroll GetById(int id) => payrollDAO.GetById(id);
    }
}
