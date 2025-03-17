using BusinessObjects.Models;
using Repositories.Interface;
using Services.InterfaceServie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public Task Add(Notification entity)
        {
            return _notificationRepository.Add(entity);
        }
    }
}
