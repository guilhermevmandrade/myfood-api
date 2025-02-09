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
    [Route("api/meal")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly IValidator<MealRequest> _mealValidator;
        private readonly IValidator<MealFoodRequest> _mealFoodValidator;

        public MealController(IMealService mealService, IValidator<MealRequest> mealValidator, IValidator<MealFoodRequest> mealFoodValidator)
        {
            _mealService = mealService;
            _mealValidator = mealValidator;
            _mealFoodValidator = mealFoodValidator;
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
        public async Task<IActionResult> RegisterMeal(MealRequest request)
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

        [HttpPost("{id}/foods/{foodId}")]
        public async Task<IActionResult> AddFoodToMeal(MealFoodRequest request, int id, int foodId)
        {
            var validationResult = _mealFoodValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _mealService.AddFoodToMealAsync(request, id, foodId, userId);

            return Ok("Alimento adicionado à refeição com sucesso!");
        }

        [HttpDelete("{id}/foods/{foodId}")]
        public async Task<IActionResult> RemoveFoodFromMeal(int id, int foodId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _mealService.RemoveFoodFromMealAsync(id, foodId, userId);

            return Ok("Alimento removido da refeição com sucesso!");
        }
    }
}
