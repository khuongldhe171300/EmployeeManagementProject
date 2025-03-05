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
    public class PositionRepository : IPositionRepository
    {
        private readonly HrmanagementContext _context;
        public PositionRepository(HrmanagementContext context)
        {
            _context = context;
        }
        public Task Add(Position entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Position>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Position> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Position entity)
        {
            throw new NotImplementedException();
        }
    }
}
