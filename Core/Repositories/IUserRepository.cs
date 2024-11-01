using Core.Models;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(string userId, User user);
        Task<bool> DeleteUserAsync(string userId);
    }
}
