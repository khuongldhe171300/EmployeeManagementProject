using BusinessObjects.Models;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ActivityLoggerService
    {
        private readonly ActivityLoggerReposirory _activityLoggerReposirory;
        public ActivityLoggerService(ActivityLoggerReposirory activityLoggerReposirory)
        {
            _activityLoggerReposirory = activityLoggerReposirory;
        }

        public void LogActivity(int userId, string action, string actionDetails, string? ipAddress = null)
        {
            _activityLoggerReposirory.LogActivity(userId, action, actionDetails, ipAddress);
        }

        public async Task<ActivityLog> GetActivityLogById(int id)
        {
            return await _activityLoggerReposirory.GetActivityLogById(id);
        }

        public  User GetById(int id)
        {
            return  _activityLoggerReposirory.GetById(id);
        }

        public async Task<List<ActivityLog>> GetActivityLogs()
        {
            return await _activityLoggerReposirory.GetActivityLogs();
        }

        public List<ActivityLog> GetAllActivityLogs()
        {
            return _activityLoggerReposirory.GetAllActivityLogs();
        }
    }
}
