using MyFood.Models;

namespace MyFood.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User food);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(User food);
        Task<bool> DeleteAsync(int id);
    }
}