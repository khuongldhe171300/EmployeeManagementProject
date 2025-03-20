using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssetObjects
{
    public class ActivityLoggerDAO
    {
        private readonly HrmanagementContext _context;

        public ActivityLoggerDAO(HrmanagementContext context)
        {
            _context = context;
        }

        public async Task LogActivity(int userId, string action, string actionDetails, string? ipAddress = null)
        {
            var existingLog = await _context.ActivityLogs.FirstOrDefaultAsync(x => x.UserId == userId);

            if (existingLog != null)
            {

                existingLog.Action += $" - {action}";
                existingLog.ActionDetails += $" - {actionDetails}";
                existingLog.LoggedAt = DateTime.UtcNow;
            }
            else
            {
                // Nếu chưa có log, tạo mới
                var newLog = new ActivityLog
                {
                    UserId = userId,
                    Action = action,
                    ActionDetails = actionDetails,
                    Ipaddress = ipAddress ?? "Unknown",
                    LoggedAt = DateTime.UtcNow
                };

                _context.ActivityLogs.Add(newLog);
            }

            await _context.SaveChangesAsync(); 
        }


        public async Task<ActivityLog> GetActivityLogById(int id)
        {
            return await _context.ActivityLogs.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.EmployeeId == id);
        }

    }

}
