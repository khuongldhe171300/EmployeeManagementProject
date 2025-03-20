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
        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _notificationRepository.GetAll();
        }
        public async Task<IEnumerable<Notification>> GetById2(int empID)
        {
            return await _notificationRepository.GetById2(empID);
        }
        public async Task<Notification> GetById(int empID)
        {
            return await _notificationRepository.GetById(empID);
        }
        public Task Update(Notification entity)
        {
            return _notificationRepository.Update(entity);
        }

        public void SendNotificationToEmployee(int employeeId, string title, string content, string type)
        {
            _notificationRepository.SendNotificationToEmployee(employeeId, title, content, type);
        }

            public void SendNotificationToAllEmployee(string title, string content, string type)
        {
            _notificationRepository.SendNotificationToAllEmployee(title, content, type);
        }
    }
}
