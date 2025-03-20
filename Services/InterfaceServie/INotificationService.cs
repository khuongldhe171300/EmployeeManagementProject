using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServie
{
    public interface INotificationService
    {
        Task Add(Notification entity);
        Task<IEnumerable<Notification>> GetAll();
        Task<IEnumerable<Notification>> GetById2(int empID);
        Task<Notification> GetById(int empID);
        Task Update(Notification entity);

    }
}
