using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class UserService
    {

        private readonly UserRepository _userRepo;
        public UserService(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

       
    }
}
