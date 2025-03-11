using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssetObjects
{
    public class EmployeeDAO
    {
        private readonly HrmanagementContext _context;

        public EmployeeDAO()
        {
            _context = new HrmanagementContext();
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employees
                            .Include(e => e.Department)
                            .Include(e => e.Position)
                            .ToList();
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public bool EmailExisting(string mail)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Email.Equals(mail));
            if (employee == null) return false;
            return true;
        }

        public void UpdateEmployeeById(Employee employee)
        {
            var employeeExisting = _context.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (employeeExisting == null) throw new Exception("Nhân viên không tồn tại");
            employeeExisting.EmployeeId = employee.EmployeeId;
            employeeExisting.FullName = employee.FullName;
            employeeExisting.DateOfBirth = employee.DateOfBirth;
            employeeExisting.Gender = employee.Gender;
            employeeExisting.Email = employee.Email;
            employeeExisting.PhoneNumber = employee.PhoneNumber;
            employeeExisting.Address = employee.Address;
            employeeExisting.Avatar = employee.Avatar;
            employeeExisting.DepartmentId = employee.DepartmentId;
            employeeExisting.StartDate = employee.StartDate;
            employeeExisting.PositionId = employee.PositionId;
            _context.Employees.Update(employeeExisting);
            _context.SaveChanges();
        }
    }
}
