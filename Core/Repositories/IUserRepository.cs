using Core.Models;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(string id, User user);
        Task<bool> DeleteUserAsync(string id);
    }
}
