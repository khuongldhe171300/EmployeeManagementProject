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
        private readonly LeaveRequestDAO leaveRequestDAO;

        public LeaveRequestRepository(LeaveRequestDAO _leaveRequestDAO)
        {
            leaveRequestDAO = _leaveRequestDAO;
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

        public List<LeaveSummary> GetLeaveSummary(int employeeId, int month, int year) => leaveRequestDAO.GetLeaveSummary(employeeId, month, year);

        public Task Update(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
