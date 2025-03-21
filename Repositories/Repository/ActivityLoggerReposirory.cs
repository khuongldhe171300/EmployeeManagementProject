using BusinessObjects.Models;
using DataAssetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class ActivityLoggerReposirory
    {

        private readonly ActivityLoggerDAO _activityLogger;
        public ActivityLoggerReposirory(ActivityLoggerDAO activityLogger)
        {
            _activityLogger = activityLogger;
        }

        public void LogActivity(int userId, string action, string actionDetails, string? ipAddress = null)
        {
            _activityLogger.LogActivity(userId, action, actionDetails, ipAddress);
        }

        public async Task<ActivityLog> GetActivityLogById(int id)
        {
            return await _activityLogger.GetActivityLogById(id);
        }

        public  User GetById(int id)
        {
            return  _activityLogger.GetById(id);
        }

        public async Task<List<ActivityLog>> GetActivityLogs()
        {
            return await _activityLogger.GetActivityLogs();
        }

        public List<ActivityLog> GetAllActivityLogs()
        {
            return _activityLogger.GetAllActivityLogs();
        }
    }
}
