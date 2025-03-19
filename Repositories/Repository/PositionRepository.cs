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
        private readonly PositionDAO _positionDAO;
        public PositionRepository()
        {
            _positionDAO = new PositionDAO();
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

        public List<Position> GetPositions()
        {
           return _positionDAO.GetPositions();
        }

        public Task Update(Position entity)
        {
            throw new NotImplementedException();
        }
    }
}
