using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using Repositories.Repository;
using Services.InterfaceServie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestService(LeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public List<LeaveSummary> GetLeaveSummary(int employeeId, int month, int year) => _leaveRequestRepository.GetLeaveSummary(employeeId, month, year); 

        public async Task<IEnumerable<LeaveRequest>> GetAll()
        {
            return await _leaveRequestRepository.GetAll();
        }

        public async Task Add(LeaveRequest entity)
        {
            await _leaveRequestRepository.Add(entity);
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestByID(int id)
        {
            return await _leaveRequestRepository.GetLeaveRequestsByEmployeeId(id);
        }

        public Task Update(LeaveRequest entity)
        {
            return _leaveRequestRepository.Update(entity);
        }

        public Task<IEnumerable<LeaveRequest>> SearchByEmployeeName(string employeeName)
        {
            return _leaveRequestRepository.SearchByEmployeeName(employeeName);
        }
    }
}
