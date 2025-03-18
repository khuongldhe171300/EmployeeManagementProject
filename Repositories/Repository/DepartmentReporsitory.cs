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
    public class DepartmentReporsitory : IDepartmentRepository
    {
        private readonly DepartmentDAO _departmentDAO;


        public DepartmentReporsitory()
        {
            _departmentDAO = new DepartmentDAO();
        }

        public Task Add(Department entity)
        {
            throw new NotImplementedException();
        }

        public void AddDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmpDep(int depID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmp()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Department? GetDepartmentByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Department> GetDepartments()
        {
            return _departmentDAO.GetDepartments();
        }

        public Department GetDepByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmpByDep(int depID)
        {
            throw new NotImplementedException();
        }

        public Task Update(Department entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmpDep(int depID, int empID)
        {
            throw new NotImplementedException();
        }
    }
}
