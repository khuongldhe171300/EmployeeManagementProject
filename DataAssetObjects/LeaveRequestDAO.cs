using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using BusinessObjects.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;  // Cần thư viện này để lấy connection string từ DbContext

namespace DataAssetObjects
{
    public class LeaveRequestDAO
    {
        private readonly string _connectionString;
        private readonly HrmanagementContext _context;

        public LeaveRequestDAO(HrmanagementContext context)
        {
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _context = context;
        }
        
        public List<LeaveSummary> GetLeaveSummary(int? employeeId, int month, int year)
        {
            List<LeaveSummary> leaveSummaries = new List<LeaveSummary>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            WITH AttendanceSummary AS (
                SELECT 
                    EmployeeID, 
                    COUNT(*) AS TotalWorkDays,
                    SUM(CAST(WorkHours AS DECIMAL(10,2))) AS TotalWorkHours
                FROM Attendance
                WHERE MONTH(WorkDate) = @Month
                    AND YEAR(WorkDate) = @Year
                    AND (@EmpID IS NULL OR EmployeeID = @EmpID)
                GROUP BY EmployeeID
            ),
            LeaveSummary AS (
                SELECT 
                    EmployeeID, 
                    COALESCE(SUM(TotalDays), 0) AS TotalLeaveDays
                FROM LeaveRequest
                WHERE MONTH(StartDate) = @Month
                    AND YEAR(StartDate) = @Year
                    AND Status = N'Đã duyệt'
                    AND (@EmpID IS NULL OR EmployeeID = @EmpID)
                GROUP BY EmployeeID
            ),
            MonthDays AS (
                SELECT DAY(EOMONTH(DATEFROMPARTS(@Year, @Month, 1))) AS TotalDaysInMonth
            )
            SELECT 
                e.EmployeeID,
                e.FullName,
                COALESCE(a.TotalWorkDays, 0) AS TotalWorkDays,
                COALESCE(l.TotalLeaveDays, 0) AS TotalLeaveDays,
                (m.TotalDaysInMonth - COALESCE(a.TotalWorkDays, 0) - COALESCE(l.TotalLeaveDays, 0)) AS TotalUnexcusedLeaveDays,
                COALESCE(a.TotalWorkHours, 0) AS TotalWorkHours
            FROM Employees e
            LEFT JOIN AttendanceSummary a ON e.EmployeeID = a.EmployeeID
            LEFT JOIN LeaveSummary l ON e.EmployeeID = l.EmployeeID
            CROSS JOIN MonthDays m
            WHERE (@EmpID IS NULL OR e.EmployeeID = @EmpID);
        ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (employeeId.HasValue && employeeId > 0)
                        cmd.Parameters.AddWithValue("@EmpID", employeeId);
                    else
                        cmd.Parameters.AddWithValue("@EmpID", DBNull.Value); // Lấy tất cả nhân viên

                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaveSummary summary = new LeaveSummary
                            {
                                FullName = reader["FullName"].ToString(),
                                TotalWorkDays = reader["TotalWorkDays"] != DBNull.Value ? Convert.ToInt32(reader["TotalWorkDays"]) : 0,
                                TotalLeaveDays = reader["TotalLeaveDays"] != DBNull.Value ? Convert.ToInt32(reader["TotalLeaveDays"]) : 0,
                                TotalUnexcusedLeaveDays = reader["TotalUnexcusedLeaveDays"] != DBNull.Value ? Convert.ToInt32(reader["TotalUnexcusedLeaveDays"]) : 0,
                                TotalWorkHours = reader["TotalWorkHours"] != DBNull.Value ? Convert.ToDouble(reader["TotalWorkHours"]) : 0.0
                            };
                            leaveSummaries.Add(summary);
                        }
                    }
                }
            }
            return leaveSummaries;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAll()
        {
            return await _context.LeaveRequests
                .Include(l => l.Employee)
                .ToListAsync();
        }

        public async Task Add(LeaveRequest entity)
        {
            _context.LeaveRequests.Add(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeId(int employeeId)
        {
            return await _context.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => l.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task Update(LeaveRequest entity)
        {
            _context.LeaveRequests.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> SearchByEmployeeName(string employeeName)
        {
            return await _context.LeaveRequests
                .Where(lr => lr.Employee.FullName.Contains(employeeName))
                .ToListAsync();
        }



    }

    public class LeaveSummary
    {
        public string FullName { get; set; }
        public int TotalWorkDays { get; set; }
        public int TotalLeaveDays { get; set; }
        public int TotalUnexcusedLeaveDays { get; set; }
        public double TotalWorkHours { get; set; }
    }
}
