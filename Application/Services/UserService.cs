using Core.Models;
using Core.Repositories;
using Core.Services;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;

        public UserService(IUserRepository userRepository, IImageService imageService)
        {
            _userRepository = userRepository;
            _imageService = imageService;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            User? user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException("Usuario não encontrado");
            }

            return user;
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

        public async Task<User> UpdateUser(string id, User user)
        {
            if (user == null || string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("User ou User Id não pode ser nulo");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new ArgumentException("Usuario não encontrado");
            }

            existingUser.Id = id;
            existingUser.Nome = user.Nome;
            existingUser.Apelido = user.Apelido;
            existingUser.Cpf = user.Cpf;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Genero = user.Genero;
            existingUser.Telefone = user.Telefone;
            existingUser.Email = user.Email;
            existingUser.Senha = user.Senha;

            await _userRepository.UpdateUserAsync(id, existingUser);

            return existingUser;
        }

        public async Task<bool> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("UserID não pode ser nulo");
            }

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException("Usuario não existe");
            }

            await _userRepository.DeleteUserAsync(id);

            return true;
        }

        public async Task<User> GetUserById(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");
            return user;
        }

        public async Task<string> UploadProfilePicture(string userId, FileData file)
        {
            User user = await GetUserById(userId);
            string uploadedFileUrl = await _imageService.UploadImage(file, "users", $"{user.Apelido}");
            user.ImageUrl = uploadedFileUrl;
            await _userRepository.UploadImage();

            return uploadedFileUrl;
        }


    }
}
