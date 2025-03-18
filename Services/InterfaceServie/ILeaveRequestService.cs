
using BusinessObjects.Models;
using DataAssetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServie
{
    public interface ILeaveRequestService
    {
        List<LeaveSummary> GetLeaveSummary(int employeeId, int month, int year);
        Task<IEnumerable<LeaveRequest>> GetAll();
        Task Add(LeaveRequest entity);
        Task Update(LeaveRequest entity);

        Task<IEnumerable<LeaveRequest>> SearchByEmployeeName(string employeeName);

    }
}
