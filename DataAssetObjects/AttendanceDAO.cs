using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssetObjects
{
    public class AttendanceDAO
    {
        private readonly HrmanagementContext _context;

        public AttendanceDAO (HrmanagementContext context)
        {
            _context = context;
        }

        public Attendance? GetAttendanceByEmployeeId(int id)
        {
            try
            {
                return _context.Attendances.FirstOrDefault(x => x.EmployeeId == id);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi (tuỳ vào hệ thống logging của bạn)
                Console.WriteLine($"Lỗi khi lấy dữ liệu chấm công: {ex.Message}");
                return null; // Trả về null hoặc có thể throw exception tuỳ yêu cầu
            }
        }

    }
}
