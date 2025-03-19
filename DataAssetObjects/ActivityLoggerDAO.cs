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

        public void LogActivity(int userId, string action, string actionDetails, string? ipAddress = null)
        {
            var log = new ActivityLog
            {
                UserId = userId,
                Action = action,
                ActionDetails = actionDetails,
                Ipaddress = ipAddress ?? "Unknown",
                LoggedAt = DateTime.UtcNow
            };

            _context.ActivityLogs.Add(log);
            _context.SaveChanges();
        }
    }

}
