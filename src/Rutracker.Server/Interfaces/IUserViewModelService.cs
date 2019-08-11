﻿using System.Security.Claims;
using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Server.Interfaces
{
    public interface IUserViewModelService
    {
        Task<UserViewModel[]> GetUsersAsync();
        Task<UserViewModel> GetUserAsync(ClaimsPrincipal principal);
        Task UpdateUserAsync(ClaimsPrincipal principal, EditUserViewModel model);
    }
}