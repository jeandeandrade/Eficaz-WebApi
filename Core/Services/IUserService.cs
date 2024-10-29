using Core.Models;

namespace Core.Services
{
    public interface IUserService
    {
        public Task<User> AddUser(User user);
        public Task<User> UpdateUser(string userId, User user);
        public Task<bool> DeleteUser(string userId);
    }
}
