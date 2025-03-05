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
    public class ActivityRepository :IActivityRepository
    {
        private readonly HrmanagementContext _context;
        public ActivityRepository(HrmanagementContext context)
        {
            _context = context;
        }

        public Task Add(ActivityLog entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityLog>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ActivityLog> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(ActivityLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
