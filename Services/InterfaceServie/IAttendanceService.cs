using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServie
{
    public interface IAttendanceService
    {
        List<Attendance> GetAttendanceByEmployeeId(int id);
        void UpdateAttendance(Attendance attendance);
        void AddAttendance(Attendance attendance);
    }
}
