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

        private readonly HrmanagementContext _context;
        public NotificationRepository(HrmanagementContext context)
        {
            _context = context;
        }
        public Task Add(Notification entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task Update(Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}
