using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly NotificationDAO _notificationDAO;
        public NotificationRepository(NotificationDAO notificationDAO)
        {
            _notificationDAO = notificationDAO;
        }
        public Task Add(Notification entity)
        {
            return _notificationDAO.Add(entity);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notification>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetById(int id)
        {
            return (Task<Notification>)_notificationDAO.GetById(id);
        }
        public Task<IEnumerable<Notification>> GetById2(int empID)
        {
            return _notificationDAO.GetById2(empID);
        }
        public Task Update(Notification entity)
        {
            return _notificationDAO.Update(entity);
        }
    }
}
