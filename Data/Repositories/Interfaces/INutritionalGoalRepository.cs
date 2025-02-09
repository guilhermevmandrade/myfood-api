using MyFood.DTOs.Responses;
using MyFood.Models;
using MyFood.Models.Enums;

namespace MyFood.Data.Repositories.Interfaces
{
    public interface INutritionalGoalRepository
    {
        Task CreateNutritionalGoalAsync(NutritionalGoal nutritionalGoal);
        Task<DailyCaloriesResponse?> GetDailyCaloriesAsync(int userId);
        Task UpdateDailyCaloriesAsync(int userId, int dailyCalories, GoalEnum weightGoal);
        Task<MacrosPercentageResponse?> GetMacrosAsync(int userId);
        Task UpdateMacrosAsync(int userId, int proteinsPercentage, int carbsPercentage, int fatsPercentage);
        Task<bool> NutritionalGoalExistsAsync(int userId);
    }
}
