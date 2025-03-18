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
    public class DepartmentService : IDepartmentService
    {
        private readonly DepartmentRepository departmentRepository;
        public DepartmentService(DepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public void AddDepartment(Department department) => departmentRepository.AddDepartment(department);

        public void DeleteDepartment(int id) => departmentRepository.DeleteDepartment(id);

        public Department? GetDepartmentByID(int id) => departmentRepository.GetDepartmentByID(id);

        public List<Department> GetDepartments() => departmentRepository.GetDepartments();
        public Department GetDepByName(string name) => departmentRepository.GetDepByName(name);

        public void UpdateDepartment(Department department) => departmentRepository.UpdateDepartment(department);
        public List<Employee> GetEmpByDep(int depID) => departmentRepository.GetEmpByDep(depID);
        public void UpdateEmpDep(int depID, int empID) => departmentRepository.UpdateEmpDep(depID, empID);
        public void DeleteEmpDep(int depID) => departmentRepository.DeleteEmpDep(depID);
        public List<Employee> GetAllEmp() => departmentRepository.GetAllEmp();
    }
}
