using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;

namespace MyFood.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponse> AuthenticateAsync(string email, string password);
        Task RegisterAsync(RegisterRequest request);
        Task<GetUserResponse> GetUserAsync(int id);
        Task UpdateUserAsync(UpdateUserRequest request, int id);
        Task DeleteUserAsync(int id, string email, string password);
    }
}
