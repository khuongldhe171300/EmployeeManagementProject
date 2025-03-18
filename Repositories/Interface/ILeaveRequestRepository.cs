using BusinessObjects.Models;
using DataAssetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
        List<LeaveSummary> GetLeaveSummary(int employeeId, int month, int year);
    }
}
