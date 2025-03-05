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
    public class PayrollRepository : IPayrollRepository
    {

        private readonly HrmanagementContext _context;
        public PayrollRepository(HrmanagementContext context)
        {
            _context = context;
        }
        public Task Add(Payroll entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payroll>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Payroll> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Payroll entity)
        {
            throw new NotImplementedException();
        }
    }
}
