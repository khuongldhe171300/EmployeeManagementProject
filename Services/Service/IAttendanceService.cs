using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface IAttendanceService
    {
        List<Attendance> GetAttendanceByEmployeeId(int id);
    }
}
