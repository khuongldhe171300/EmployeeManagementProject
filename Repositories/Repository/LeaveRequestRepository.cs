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
           return leaveRequestDAO.Add(entity);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LeaveRequest>> GetAll()
        {
           return await leaveRequestDAO.GetAll();
        }

        public Task<LeaveRequest> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<LeaveSummary> GetLeaveSummary(int employeeId, int month, int year) => leaveRequestDAO.GetLeaveSummary(employeeId, month, year);

        public Task Update(LeaveRequest entity)
        {
            return leaveRequestDAO.Update(entity);
        }

        public Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeId(int id) => leaveRequestDAO.GetLeaveRequestsByEmployeeId(id);

        public Task<IEnumerable<LeaveRequest>> SearchByEmployeeName(string employeeName) => leaveRequestDAO.SearchByEmployeeName(employeeName);
        
    }
}
