using Core.Services;
using Core.Models;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userDTO)
        {
            string userId = _authService.GetAuthenticatedUserId(User)!;
            User user = await _userService.AddUser(userId, userDTO);

            return CreatedAtAction(nameof(PostOrder), new { id = order.Id }, order);
        }
    }
}

