using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Services.InterfaceServie
{
    public interface IEmployeeService
    {
        Employee GetEmployeeByID(int id);
        void UpdateEmployee(Employee employee);
        User GetUserByEmpID(int empID);
    }
}
