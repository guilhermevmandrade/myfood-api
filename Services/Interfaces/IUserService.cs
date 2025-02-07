﻿using MyFood.DTOs.Responses;
using MyFood.Models;

namespace MyFood.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponse> AuthenticateAsync(string email, string password);
        Task RegisterAsync(string name, string email, string password);
    }
}
