using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetById2(int id)
        {
            return await _context.Notifications.Where(x => x.ReceiverId == id).ToListAsync();
        }
        public async Task GetById(int id)
        {
            await _context.Notifications.FindAsync(id);
        }
        public async Task Update(Notification notification)
        {
            using (var context = new HrmanagementContext())
            {
                context.Entry(notification).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public void SendNotificationToEmployee(int employeeId, string title, string content, string type)
        {
            var notification = new Notification
            {
                ReceiverId = employeeId,
                Title = title,
                Content = content,
                NotificationType = type,
                CreatedDate = DateTime.Now,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void SendNotificationToAllEmployees(string title, string content, string type)
        {
            var allEmployeeIds = _context.Employees.Select(e => e.EmployeeId).ToList();

            var notifications = allEmployeeIds.Select(empId => new Notification
            {
                ReceiverId = empId,
                Title = title,
                Content = content,
                NotificationType = type,
                CreatedDate = DateTime.Now,
                IsRead = false
            }).ToList();

            _context.Notifications.AddRange(notifications);
            _context.SaveChanges();
        }
    }
}
