using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Models.Enums;

namespace MyFood.Services.Interfaces
{
    public interface INutritionalGoalService
    {
        Task DefineNutritionalGoalAsync(NutritionalGoalRequest request, int userId);
        Task<DailyCaloriesResponse> GetDailyCaloriesAsync(int userId);
        Task UpdateDailyCaloriesAsync(DailyCaloriesRequest request, int userId);
        Task<MacrosPercentageResponse> GetMacrosAsync(int userId);
        Task UpdateMacrosAsync(MacrosPercentageRequest request, int userId);
        Task<DailyCaloriesResponse> SuggestDailyCalories(int userId, GoalEnum weightGoal);
    }
}
