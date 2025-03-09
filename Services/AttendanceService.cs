using BusinessObjects.Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepo;

        public AttendanceService (IAttendanceRepository attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }   

        public Attendance? GetAttendanceByEmployeeId(int id) => _attendanceRepo.GetAttendanceByEmployeeId(id);
    }
}
