using Core.Models;
using Core.Repositories;
using Core.Services;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Id = Guid.NewGuid().ToString();
            await _userRepository.AddUserAsync(user);

            return user;
        }

        public async Task<User> UpdateUser(string userId, User user)
        {
            if (user == null || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("User ou User Id não pode ser nulo");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                throw new ArgumentException("Usuario não encontrado");
            }

            existingUser.Id = userId;
            existingUser.Nome = user.Nome;
            existingUser.Apelido = user.Apelido;
            existingUser.Cpf = user.Cpf;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Genero = user.Genero;
            existingUser.Telefone = user.Telefone;
            existingUser.Email = user.Email;
            existingUser.Senha = user.Senha;

            await _userRepository.UpdateUserAsync(userId, existingUser);

            return existingUser;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("UserID não pode ser nulo");
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("Usuario não existe");
            }

            await _userRepository.DeleteUserAsync(userId);

            return true;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            User? user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("Usuario não encontrado");
            }

            return user;
        }
    }
}
