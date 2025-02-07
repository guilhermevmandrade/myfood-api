using MyFood.Models;

namespace MyFood.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}