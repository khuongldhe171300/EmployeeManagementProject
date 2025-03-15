using DataAssetObjects;
using Repositories.Interface;
using Repositories.Repository;
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
    }
}
