using BusinessObjects.Models;
using Microsoft.Data.SqlClient;
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

        public AttendanceDAO(HrmanagementContext context)
        {
            _context = context;
        }

        public List<Attendance> GetAttendanceByEmployeeId(int id)
        {
            try
            {
                return _context.Attendances.Where(x => x.EmployeeId == id).ToList();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi (tuỳ vào hệ thống logging của bạn)
                Console.WriteLine($"Lỗi khi lấy dữ liệu chấm công: {ex.Message}");
                return new List<Attendance>(); // Trả về danh sách rỗng thay vì null để tránh lỗi NullReferenceException
            }
        }

        public void AddAttendance(Attendance attendance)
        {
            try
            {
                _context.Attendances.Add(attendance);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi (tuỳ vào hệ thống logging của bạn)
                Console.WriteLine($"Lỗi khi lấy dữ liệu chấm công: {ex.Message}");
            }
        }

        public void UpdateAttendance(Attendance attendance)
        {
            try
            {
                _context.Attendances.Update(attendance);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi (tuỳ vào hệ thống logging của bạn)
                Console.WriteLine($"Lỗi khi lấy dữ liệu chấm công: {ex.Message}");
            }
        }

    }
}
