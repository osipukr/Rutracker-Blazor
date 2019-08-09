﻿using System.Threading.Tasks;
using Rutracker.Core.Entities.Identity;

namespace Rutracker.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<User> CreateUserAsync(string userName, string password);
        Task<User> CheckUserAsync(string userName, string password);
        Task AddUserToRoleAsync(string userId, string role);
        Task UpdateUserAsync(User user);
        Task LogOutUserAsync();
    }
}