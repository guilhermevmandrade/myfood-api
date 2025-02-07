using Microsoft.AspNetCore.Mvc;
using MyFood.DTOs.Requests;
using MyFood.Services.Interfaces;

namespace MyFood.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _userService.RegisterAsync(request.Name, request.Email, request.Password);

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResponse = await _userService.AuthenticateAsync(request.Email, request.Password);

            return Ok(authResponse);
        }
    }
}
