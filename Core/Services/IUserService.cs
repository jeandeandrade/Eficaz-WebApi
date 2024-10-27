using Core.Models;

namespace Core.Services
{
    public interface IUserService
    {
        public Task<User> GetUser(string userId);
        public Task<User> AddUser(string userId, User user);
        public Task<User> UpdateUser(string userId, User user);
        public Task<bool> DeleteUser(string userId);
    }
}
