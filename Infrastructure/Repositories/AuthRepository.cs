using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        private readonly EficazDbContext _context;

        public AuthRepository(EficazDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByEmailAndPassword(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Senha == password);
        }

    }
}
