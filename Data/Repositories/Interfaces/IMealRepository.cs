using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Models;

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
    }
}