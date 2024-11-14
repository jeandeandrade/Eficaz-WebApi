using Core.Models;
using Core.Repositories;
using Core.Services;
using System.Security.Claims;

namespace Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public string? GetAuthenticatedUserId(ClaimsPrincipal User)
        {
            return User.FindFirst("id")?.Value;
        }

        public async Task<string> SignIn(string email, string password)
        {
            User user = await _authRepository.GetUserByEmailAndPassword(email, password);
            if (user == null)
            {
                throw new Exception("Usuário e/ou senha inválidos");
            }

            string token = _tokenService.CreateUserToken(user);

            return token;
        }

    }
}