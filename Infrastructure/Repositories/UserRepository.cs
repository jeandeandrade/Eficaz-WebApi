using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EficazDbContext _context;

        public UserRepository(EficazDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUserAsync(string userId, User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (existingUser == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            existingUser.Nome = user.Nome;
            existingUser.Apelido = user.Apelido;
            existingUser.Cpf = user.Cpf;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Genero = user.Genero;
            existingUser.Telefone = user.Telefone;
            existingUser.Email = user.Email;
            existingUser.Senha = user.Senha;
            existingUser.Addresses = user.Addresses;

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
