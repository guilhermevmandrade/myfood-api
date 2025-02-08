using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Models;

namespace MyFood.Data.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        Task CreateAsync(Food food);
        Task<IEnumerable<FoodResponse>> GetAllByUserIdAsync(int userId);
        Task<FoodResponse?> GetUserFoodByIdAsync(int id, int userId);
        Task UpdateAsync(FoodRequest food, int id);
        Task DeleteAsync(int id);
    }
}