using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFood.DTOs.Requests;
using MyFood.Models;
using MyFood.Services.Interfaces;
using System.Security.Claims;

namespace MyFood.Controllers
{
    [ApiController]
    [Route("api/food")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;
        private readonly IValidator<FoodRequest> _foodValidator;

        public FoodController(IFoodService foodService, IValidator<FoodRequest> foodValidator)
        {
            _foodService = foodService;
            _foodValidator = foodValidator;
        }

        [HttpGet]
        public async Task<IActionResult> ListUserFoods()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var foodList = await _foodService.ListUserFoodAsync(userId);

            return Ok(foodList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFood(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var food = await _foodService.GetUserFoodAsync(id, userId);

            return Ok(food);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterFood(FoodRequest request)
        {
            var validationResult = _foodValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _foodService.RegisterFoodAsync(request, userId);

            return Ok("Alimento cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(FoodRequest request, int id)
        {
            var validationResult = _foodValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _foodService.UpdateFoodAsync(request, id, userId);

            return Ok("Alimento ataualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _foodService.DeleteFoodAsync(id, userId);

            return Ok("Alimento deletado com sucesso!");
        }
    }
}
