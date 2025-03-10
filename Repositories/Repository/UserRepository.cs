using BusinessObjects.Models;
using DataAssetObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        //Thaodp
        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            using (HrmanagementContext _context = new HrmanagementContext())
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == userName && u.PasswordHash == password);

                if (user == null)
                {
                    throw new KeyNotFoundException("Không tìm thấy thông tin người dùng");
                }

                if (!user.IsActive)
                {
                    throw new UnauthorizedAccessException("Tài khoản không có quyền truy cập");
                }

                return user;
            }
           
        }


        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
