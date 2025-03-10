using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using Repositories.Repository;
using Services.Service;
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

//        public AttendanceService(AttendanceRepository attendanceRepo)
//        {
//            _attendanceRepo = attendanceRepo;
//        }

        public void AddAttendance(Attendance attendance) => _attendanceRepo.AddAttendance(attendance);

        public void UpdateAttendance(Attendance attendance) => _attendanceRepo.UpdateAttendance(attendance);

        List<Attendance> IAttendanceService.GetAttendanceByEmployeeId(int id) => _attendanceRepo.GetAttendanceByEmployeeId(id);
    }
}
