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
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HrmanagementContext _context;
        public AttendanceRepository(HrmanagementContext context)
        {
            _context = context;
        }
        public Task Add(Attendance entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Attendance>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Attendance> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Attendance entity)
        {
            throw new NotImplementedException();
        }
    }
}
