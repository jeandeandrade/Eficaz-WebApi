using Core.Models;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUser(string userId);
    }
}
