using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFood.DTOs.Requests;
using MyFood.Services.Interfaces;
using System.Security.Claims;

namespace MyFood.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UpdateUserRequest> _updateUserValidator;
        private readonly IValidator<DeleteUserRequest> _deleteUserValidator;

        public UserController(IUserService userService, IValidator<UpdateUserRequest> updateUserValidator, IValidator<DeleteUserRequest> deleteUserValidator)
        {
            _userService = userService;
            _updateUserValidator = updateUserValidator;
            _deleteUserValidator = deleteUserValidator;
        }


        [HttpGet("me")]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var user = await _userService.GetUserAsync(userId);

            return Ok(user);
        }


        [HttpPut("me")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var validationResult = _updateUserValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _userService.UpdateUserAsync(request, userId);

            return Ok("Usuário atualizado com sucesso!");
        }


        [HttpDelete("me")]
        public async Task<IActionResult> DeleteUser(DeleteUserRequest request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("Falha ao obter o e-mail do usuário autenticado.");
            }

            var validationResult = _deleteUserValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _userService.DeleteUserAsync(userId, userEmail, request.Password);

            return Ok("Usuário deletado com sucesso!");
        }
    }
}
