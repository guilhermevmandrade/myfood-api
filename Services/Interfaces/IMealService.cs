using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;

namespace MyFood.Services.Interfaces
{
    public interface IMealService
    {
        Task RegisterMealAsync(MealRequest request, int userId);
        Task<List<MealResponse>> ListUserMealAsync(int userId);
        Task<MealResponse> GetUserMealAsync(int mealId, int userId);
        Task UpdateMealAsync(MealRequest request, int mealId, int userId);
        Task DeleteMealAsync(int mealId, int userId);
        Task AddFoodToMealAsync(MealFoodRequest request, int mealId, int foodId, int userId);
        Task RemoveFoodFromMealAsync(int mealId, int foodId, int userId);
    }
}
