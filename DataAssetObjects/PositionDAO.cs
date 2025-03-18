using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssetObjects
{
    public class PositionDAO
    {
        private readonly HrmanagementContext _context;

        public PositionDAO()
        {
            _context = new HrmanagementContext();
        }

        public List<Position> GetPositions()
        {
            return _context.Positions.ToList();
        }
    }
}
