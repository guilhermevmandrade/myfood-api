using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;

namespace MyFood.Services.Interfaces
{
    public interface IFoodService
    {
        Task RegisterFoodAsync(FoodRequest request, int userId);
        Task<List<FoodResponse>> ListUserFoodAsync(int userId);
        Task<FoodResponse> GetUserFoodAsync(int foodId, int userId);
        Task UpdateFoodAsync(FoodRequest request, int foodId, int userId);
        Task DeleteFoodAsync(int foodId, int userId);
    }
}
