using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFood.DTOs.Requests;
using MyFood.Services;
using MyFood.Services.Interfaces;
using System.Security.Claims;

namespace MyFood.Controllers
{
    [ApiController]
    [Route("api/meal")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly IValidator<MealRequest> _mealValidator;

        public MealController(IMealService mealService, IValidator<MealRequest> mealValidator)
        {
            _mealService = mealService;
            _mealValidator = mealValidator;
        }

        [HttpGet]
        public async Task<IActionResult> ListUserMeals()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var foodList = await _mealService.ListUserMealAsync(userId);

            return Ok(foodList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserMeal(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var food = await _mealService.GetUserMealAsync(id, userId);

            return Ok(food);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MealRequest request)
        {
            var validationResult = _mealValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _mealService.RegisterMealAsync(request, userId);

            return Ok("Refeição cadastrada com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeal(MealRequest request, int id)
        {
            var validationResult = _mealValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _mealService.UpdateMealAsync(request, id, userId);

            return Ok("Refeição ataualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _mealService.DeleteMealAsync(id, userId);

            return Ok("Refeição deletada com sucesso!");
        }
    }
}
