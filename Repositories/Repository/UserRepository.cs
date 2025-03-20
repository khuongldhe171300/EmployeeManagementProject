using BusinessObjects.Models;
using DataAssetObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HrmanagementContext _context;

        public UserRepository()
        {
            _context = new HrmanagementContext();
        }
        public UserRepository(HrmanagementContext context)
        {
            _context = context;
        }
       
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

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmployeeId == id);
        }


        //Thaodp
        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            using (HrmanagementContext _context = new HrmanagementContext())
            {
                var user = _context.Users.FirstOrDefault(u => u.Username.Equals(userName));

               

                if (user == null)
                {
                    throw new KeyNotFoundException("Không tìm thấy thông tin người dùng");
                }
                else
                {
                    if (!VerifyPassword(password, user.PasswordHash))
                    {
                        throw new UnauthorizedAccessException("Mật khẩu không chính xác");
                    }

                    if (!user.IsActive)
                    {
                        throw new UnauthorizedAccessException("Tài khoản không có quyền truy cập");
                    }
                }
                return user;
            }
           
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
