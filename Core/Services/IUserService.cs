using Core.Models;

namespace Core.Services
{
    public interface IUserService
    {
        public Task<User> GetUserByIdAsync(string id);
        public Task<User> AddUser(User user);
        public Task<User> UpdateUser(string userId, User user);
        Task<string> UploadProfilePicture(string userId, FileData file);
        public Task<bool> DeleteUser(string userId);
    }
}
