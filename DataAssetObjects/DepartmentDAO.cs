using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssetObjects
{
    public class DepartmentDAO
    {
        private readonly HrmanagementContext _context;

        public DepartmentDAO()
        {
            _context = new HrmanagementContext();
        }

        public List<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}
