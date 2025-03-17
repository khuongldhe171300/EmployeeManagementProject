using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DepartmentDAO departmentDAO;
        public DepartmentRepository(DepartmentDAO departmentDAO)
        {
            this.departmentDAO = departmentDAO;
        }

        public void AddDepartment(Department department) => departmentDAO.AddDepartment(department);

        public void DeleteDepartment(int id) => departmentDAO.DeleteDepartment(id);

        public Department? GetDepartmentByID(int id) => departmentDAO.GetDepartmentByID(id);

        public List<Department> GetDepartments() => departmentDAO.GetDepartments();
        public Department GetDepByName(string name) => departmentDAO.GetDepByName(name);

        public void UpdateDepartment(Department department) => departmentDAO.UpdateDepartment(department);
        public List<Employee> GetEmpByDep(int depID) => departmentDAO.GetEmpByDep(depID);
        public void UpdateEmpDep(int depID, int empID) => departmentDAO.UpdateEmpDep(depID, empID);
        public void DeleteEmpDep(int depID) => departmentDAO.DeleteEmpDep(depID);
        public List<Employee> GetAllEmp() => departmentDAO.getAllEmp();
    }
}
