using Core.Services;
using Core.Models;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser()
        {
            var userId = _authService.GetAuthenticatedUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            User? user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [EnableCors("AllowAll")]
        public async Task<ActionResult<User>> PostUser(UserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is required.");
            }

            // Inicializar a variável user
            User user = new User
            {
                Nome = userDto.Nome,
                Apelido = userDto.Apelido,
                Cpf = userDto.Cpf,
                DataNascimento = userDto.DataNascimento,
                Genero = userDto.Genero,
                Telefone = userDto.Telefone,
                Email = userDto.Email,
                Senha = userDto.Senha,
                Enderecos = userDto.Enderecos?.Select(a => new Address
                {
                    NomeRua = a.NomeRua,
                    Bairro = a.Bairro,
                    Cep = a.Cep,
                    Complemento = a.Complemento,
                    Cidade = a.Cidade,
                    NumeroResidencia = a.NumeroResidencia
                }).ToList()
            };

            try
            {
                // Adicionar usuário e associar endereços
                User newUser = await _userService.AddUser(user);

                if (newUser.Enderecos != null)
                {
                    foreach (var address in newUser.Enderecos)
                    {
                        address.UserId = newUser.Id;
                    }
                }

                return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao salvar os dados: {ex.Message}");
            }
        }

        [HttpPut]
        [EnableCors("AllowAll")]
        [Authorize]
        public async Task<ActionResult<User>> UpdateUser(UserDTO userDto)
        {
            var userId = _authService.GetAuthenticatedUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            User user = new User
            {
                Id = userId,
                Nome = userDto.Nome,
                Apelido = userDto.Apelido,
                Cpf = userDto.Cpf,
                DataNascimento = userDto.DataNascimento,
                Genero = userDto.Genero,
                Telefone = userDto.Telefone,
                Email = userDto.Email,
                Senha = userDto.Senha,
                Enderecos = userDto.Enderecos?.Select(a => new Address
                {
                    NomeRua = a.NomeRua,
                    Bairro = a.Bairro,
                    Cep = a.Cep,
                    Complemento = a.Complemento,
                    Cidade = a.Cidade,
                    NumeroResidencia = a.NumeroResidencia,
                    UserId = userId
                }).ToList()
            };

            try
            {
                User updatedUser = await _userService.UpdateUser(userId, user);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar os dados: {ex.Message}");
            }
        }

        [HttpDelete]
        [EnableCors("AllowAll")]
        [Authorize]
        public async Task<ActionResult> DeleteUser()
        {
            var userId = _authService.GetAuthenticatedUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                bool isDeleted = await _userService.DeleteUser(userId);
                if (isDeleted)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao deletar o usuário: {ex.Message}");
            }
        }

        [HttpPost("{userId}/UploadImage")]
        [EnableCors("AllowAll")]
        [Authorize]
        public async Task<ActionResult<string>> UploadProfilePicture(string userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No image found");
            }
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileData = new FileData
            {
                FileName = file.FileName,
                Content = memoryStream.ToArray(),
                ContentType = file.ContentType,
                Extension = Path.GetExtension(file.FileName),
            };
            string imageUrl = await _userService.UploadProfilePicture(userId, fileData);
            return CreatedAtAction(nameof(UploadProfilePicture), new { userId = userId }, imageUrl);
        }
    }
}
