using Core.Services;
using Core.Models;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<User> GetUser(string userId)
        {
            User? user = await _userService.GetUserByIdAsync(userId);

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            string userId = _authService.GetAuthenticatedUserId(User)!;
            User newUser = await _userService.AddUser(user);

            return CreatedAtAction(nameof(newUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            string userId = _authService.GetAuthenticatedUserId(User)!;
            User newUser = await _userService.AddUser(user);

            return CreatedAtAction(nameof(newUser), new { id = newUser.Id }, newUser);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            bool isDeleted = await _userService.DeleteUser(userId);
            
            if (isDeleted)
            {
                return Ok();
            }
            
            return BadRequest();
        }
    }
}

