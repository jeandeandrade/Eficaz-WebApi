using Core.Services;
using Core.Models;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Presentation.Controllers
{
    [ApiController]
    [Authorize]
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
        [EnableCors("AllowAll")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            foreach (var address in user.Addresses)
            {
                address.User = user;
                address.UserId = user.Id;
            }

            User newUser = await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut]
        [EnableCors("AllowAll")]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            var userId = _authService.GetAuthenticatedUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            foreach (var address in user.Addresses)
            {
                address.User = user;
                address.UserId = userId; 
            }

            User updatedUser = await _userService.UpdateUser(userId, user);
            return Ok(updatedUser);
        }


        [HttpDelete]
        [EnableCors("AllowAll")]
        public async Task<ActionResult> DeleteUser()
        {
            var userId = _authService.GetAuthenticatedUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool isDeleted = await _userService.DeleteUser(userId);
            if (isDeleted)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
