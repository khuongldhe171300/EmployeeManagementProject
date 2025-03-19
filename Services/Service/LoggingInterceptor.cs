using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class LoggingInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return base.SavedChangesAsync(eventData, result, cancellationToken);

            var logs = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .Select(e => new ActivityLog
                {
                    UserId = GetCurrentUserId(), // Lấy ID của người dùng hiện tại
                    Action = e.State switch
                    {
                        EntityState.Added => "Thêm",
                        EntityState.Modified => "Sửa",
                        EntityState.Deleted => "Xóa",
                        _ => "Khác"
                    },
                    ActionDetails = $"Bảng: {e.Entity.GetType().Name}, Hành động: {e.State}",
                    Ipaddress = GetCurrentIpAddress(), // Lấy IP hiện tại
                    LoggedAt = DateTime.Now
                }).ToList();

            if (logs.Any())
            {
                context.Set<ActivityLog>().AddRange(logs);
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private int GetCurrentUserId()
        {
            // Giả lập ID người dùng, thực tế lấy từ session hoặc authentication
            return 1;
        }

        private string GetCurrentIpAddress()
        {
            // Giả lập địa chỉ IP
            return "127.0.0.1";
        }
    }
}
