using Core.Models;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(string userId, User user);
        Task<bool> DeleteUserAsync(string userId);
    }
}
