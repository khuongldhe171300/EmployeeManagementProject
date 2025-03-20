using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetById2(int empID);

        void SendNotificationToEmployee(int employeeId, string title, string content, string type);
        void SendNotificationToAllEmployee(string title, string content, string type);
    }
}
