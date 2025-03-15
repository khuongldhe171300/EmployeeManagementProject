using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAssetObjects
{
    public class EmployeeDAO
    {
        private readonly HrmanagementContext _context;
        public EmployeeDAO(HrmanagementContext context)
        {
            _context = context;
        }
        public Employee GetEmployeeByID(int id)
        {
            return _context.Employees.Include(e => e.Position).Include(e => e.Department).FirstOrDefault(e => e.EmployeeId == id);
        }
        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }
        public User GetUserByEmpID(int empID)
        {
            return _context.Users.FirstOrDefault(u => u.EmployeeId == empID);
        }
    }
}
