using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using DataAssetObjects;

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


        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
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

        public List<DepartmentReport> GetEmployeeCountByDepartment()
        {
            return _context.Employees
                .GroupBy(e => e.DepartmentId)
                .Select(g => new DepartmentReport
                {
                    DepartmentName = _context.Departments
                        .Where(d => d.DepartmentId == g.Key)
                        .Select(d => d.DepartmentName)
                        .FirstOrDefault() ?? "Không xác định",
                    EmployeeCount = g.Count()
                })
                .ToList();
        }

        public List<PositionReport> GetEmployeeCountByPosition()
        {
            return _context.Employees
                .GroupBy(e => e.PositionId)
                .Select(g => new PositionReport
                {
                    PositionName = _context.Positions
                        .Where(p => p.PositionId == g.Key)
                        .Select(p => p.PositionName)
                        .FirstOrDefault() ?? "Không xác định",
                    EmployeeCount = g.Count()
                })
                .ToList();
        }


        public List<GenderReport> GetEmployeeCountByGender()
        {
            return _context.Employees
                .GroupBy(e => e.Gender)
                .Select(g => new GenderReport
                {
                    Gender = g.Key,
                    EmployeeCount = g.Count()
                })
                .ToList();
        }




        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.Include(e => e.Position).Include(e => e.Department).ToListAsync();
        }

    }

    public class DepartmentReport
    {
        public string DepartmentName { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class PositionReport
    {
        public string PositionName { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class GenderReport
    {
        public string Gender { get; set; }
        public int EmployeeCount { get; set; }
    }

}



