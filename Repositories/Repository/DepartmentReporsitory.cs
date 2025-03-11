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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Department> GetDepartments()
        {
            return _departmentDAO.GetDepartments();
        }

        public Task Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
