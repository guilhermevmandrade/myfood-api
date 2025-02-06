using MyFood.Models;

namespace MyFood.Data.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        Task<int> CreateAsync(Food food);
        Task<IEnumerable<Food>> GetAllAsync();
        Task<Food?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Food food);
        Task<bool> DeleteAsync(int id);
    }
}