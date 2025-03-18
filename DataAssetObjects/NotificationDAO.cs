using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssetObjects
{
    public class NotificationDAO
    {
        private readonly HrmanagementContext _context;
        public NotificationDAO(HrmanagementContext context)
        {
            _context = context;
        }

        public async Task Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

    }
}
