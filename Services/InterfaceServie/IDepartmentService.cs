using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Services.InterfaceServie
{
    public interface IDepartmentService
    {
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int id);
        Department? GetDepartmentByID(int id);
        Department GetDepByName(string name);
        List<Department> GetDepartments();
        List<Employee> GetEmpByDep(int depID);
        void UpdateEmpDep(int depID, int empID);
        void DeleteEmpDep(int depID);
        List<Employee> GetAllEmp();
    }
}
