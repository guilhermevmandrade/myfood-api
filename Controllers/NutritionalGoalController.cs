using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFood.DTOs.Requests;
using MyFood.Models;
using MyFood.Models.Enums;
using MyFood.Services;
using MyFood.Services.Interfaces;
using System.Security.Claims;

namespace MyFood.Controllers
{
    [ApiController]
    [Route("api/goals")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NutritionalGoalController : ControllerBase
    {
        private readonly INutritionalGoalService _nutritionalGoalService;
        private readonly IValidator<NutritionalGoalRequest> _nutritionalGoalValidator;
        private readonly IValidator<DailyCaloriesRequest> _dailyCaloriesValidator;
        private readonly IValidator<MacrosPercentageRequest> _macrosPercentageValidator;

        public NutritionalGoalController(INutritionalGoalService nutritionalGoalService, IValidator<NutritionalGoalRequest> nutritionalGoalValidator, IValidator<DailyCaloriesRequest> dailyCaloriesValidator, IValidator<MacrosPercentageRequest> macrosPercentageValidator)
        {
            _nutritionalGoalService = nutritionalGoalService;
            _nutritionalGoalValidator = nutritionalGoalValidator;
            _dailyCaloriesValidator = dailyCaloriesValidator;
            _macrosPercentageValidator = macrosPercentageValidator;
        }


        [HttpPost]
        public async Task<IActionResult> DefineNutritionalGoal(NutritionalGoalRequest request)
        {
            var validationResult = _nutritionalGoalValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _nutritionalGoalService.DefineNutritionalGoalAsync(request, userId);

            return Ok("Meta nutricional cadastrada com sucesso!");
        }


        [HttpGet("calories")]
        public async Task<IActionResult> GetDailyCalories()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var dailyCalories = await _nutritionalGoalService.GetDailyCaloriesAsync(userId);

            return Ok(dailyCalories);
        }


        [HttpPut("calories")]
        public async Task<IActionResult> UpdateDailyCalories(DailyCaloriesRequest request)
        {
            var validationResult = _dailyCaloriesValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _nutritionalGoalService.UpdateDailyCaloriesAsync(request, userId);

            return Ok("Meta nutricional atualizada com sucesso!");
        }


        [HttpGet("calories/suggestion/{weightGoal}")]
        public async Task<IActionResult> SuggestDailyCalories(GoalEnum weightGoal)
        {
            if (!Enum.IsDefined(typeof(GoalEnum), weightGoal))
            {
                return BadRequest("O objetivo de peso informado é inválido.");
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var caloriesSuggestion = await _nutritionalGoalService.SuggestDailyCaloriesAsync(userId, weightGoal);
            if (caloriesSuggestion == null)
            {
                return NotFound("Não foi possível calcular a sugestão de calorias.");
            }

            return Ok(caloriesSuggestion);
        }


        [HttpGet("macros")]
        public async Task<IActionResult> GetMacrosPercentage()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            var macrosPercentage = await _nutritionalGoalService.GetMacrosAsync(userId);

            return Ok(macrosPercentage);
        }


        [HttpPut("macros")]
        public async Task<IActionResult> UpdateMacrosPercentage(MacrosPercentageRequest request)
        {
            var validationResult = _macrosPercentageValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Falha ao obter o ID do usuário autenticado.");
            }

            await _nutritionalGoalService.UpdateMacrosAsync(request, userId);

            return Ok("Meta nutricional atualizada com sucesso!");
        }
    }
}
