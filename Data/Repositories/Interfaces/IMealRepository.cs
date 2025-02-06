using MyFood.Models;

namespace MyFood.Data.Repositories.Interfaces
{
    public interface IMealRepository
    {
        Task<int> CreateAsync(Meal meal);
        Task<IEnumerable<Meal>> GetAllAsync();
        Task<Meal?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Meal meal);
        Task<bool> DeleteAsync(int id);
    }
}