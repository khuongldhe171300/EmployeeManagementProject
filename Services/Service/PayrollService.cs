using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using Services.InterfaceServie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Repositories.Repository;
using Services.InterfaceServie;

namespace Services.Service
{
    public class PayrollService : IPayrollService
    {
        private readonly PayrollRepository payrollRepository;
        public PayrollService(PayrollRepository payrollRepository)
        {
            this.payrollRepository = payrollRepository;
        }
        public List<Payroll> GetPayrollByEmp(int employeeId) => payrollRepository.GetPayrollByEmp(employeeId);
        public List<Payroll> GetPayrollByMonth(int employeeId, int month, int year) => payrollRepository.GetPayrollByMonth(employeeId, month, year);
        public int GetTotalWorkingHours(int employeeId, int month, int year) => payrollRepository.GetTotalWorkingHours(employeeId, month, year);
        public int GetTotalOutTime(int employeeId, int month, int year) => payrollRepository.GetTotalOutTime(employeeId, month, year);
        public int GetTotalSalary(int employeeId, int month, int year) => payrollRepository.GetTotalSalary(employeeId, month, year);
        public List<Payroll> GetAll() => payrollRepository.GetAll();

		public Payroll GetById(int id) => payrollRepository.GetById(id);

    }
}
