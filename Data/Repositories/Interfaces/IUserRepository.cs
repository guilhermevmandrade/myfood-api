using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Models;

namespace MyFood.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<GetUserResponse?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(UpdateUserRequest user, int id);
        Task DeleteAsync(int id);
        Task<bool> UserExistsAsync(int id);
    }
}