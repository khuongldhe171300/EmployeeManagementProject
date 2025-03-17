using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAssetObjects
{
    public class DepartmentDAO
    {
        private readonly HrmanagementContext _context;
        public DepartmentDAO(HrmanagementContext context)
        {
            _context = context;
        }
        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }
        public void UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }
        public void DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                department.Status = false;
                _context.Departments.Update(department);
                _context.SaveChanges();
            }
        }
        public Department? GetDepartmentByID(int id)
        {
            return _context.Departments.Find(id);
        }

        public Department GetDepByName(string name)
        {
            return _context.Departments.FirstOrDefault(x => x.DepartmentName == name);
        }
        public List<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }

        public List<Employee> GetEmpByDep(int depID)
        {
            return _context.Employees.Where(x => x.DepartmentId == depID).Include(e => e.Position).ToList();
        }
        public void UpdateEmpDep(int depID, int empID)
        {
            Employee emp = _context.Employees.Find(empID);
            if (emp != null)
            {
                emp.DepartmentId = depID;
                _context.Employees.Update(emp);
                _context.SaveChanges();
            }
        }
        public void DeleteEmpDep(int empID)
        {
            Employee emp = _context.Employees.Find(empID);
            if (emp != null)
            {
                emp.DepartmentId = null;
                _context.SaveChanges();
            }
        }
        public List<Employee> getAllEmp()
        {
            return _context.Employees.ToList();
        }
    }
}
