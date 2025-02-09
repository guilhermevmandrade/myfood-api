using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Models;
using MyFood.Models.Enums;

namespace MyFood.Data.Repositories.Interfaces
{
    public interface IMealRepository
    {
        Task CreateAsync(Meal meal);
        Task<IEnumerable<MealResponse>> GetAllByUserIdAsync(int userId);
        Task<MealResponse?> GetUserMealByIdAsync(int id, int userId);
        Task UpdateAsync(MealRequest meal, int id);
        Task DeleteAsync(int id);
        Task<bool> MealExistsAsync(int mealId, int userId);
        Task AddFoodToMealAsync(int mealId, int foodId, decimal quantity, MeasurementUnitEnum unit);
        Task RemoveFoodFromMealAsync(int mealId, int foodId);
    }
}