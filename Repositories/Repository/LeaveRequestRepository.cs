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
    public class LeaveRequestRepository :ILeaveRequestRepository
    {
        private readonly HrmanagementContext _context;
        public LeaveRequestRepository(HrmanagementContext context)
        {
            _context = context;
        }

        public Task Add(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveRequest>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequest> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
