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
        public void AddEmployee(Employee employee, string password)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Mật khẩu không được để trống", nameof(password));

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Employees.Add(employee);

                    var user = new User
                    {
                        Username = employee.FullName,
                        Email = employee.Email,
                        PasswordHash = HashPassword(password),
                        UserRole = "User",
                        IsActive = true,
                        Employee = employee
                    };
                    _context.Users.Add(user);

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
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


        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
