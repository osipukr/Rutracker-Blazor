﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The User API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="401">If the user is not authorized.</response>
    /// <response code="404">If the item is null.</response>
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserViewModelService _userViewModelService;

        public UsersController(IUserViewModelService userViewModelService)
        {
            _userViewModelService = userViewModelService ?? throw new ArgumentNullException(nameof(userViewModelService));
        }

        [HttpGet]
        public async Task<UserViewModel[]> GetAll() => await _userViewModelService.UsersAsync();

        [HttpGet(nameof(Details))]
        public async Task<UserViewModel> Details() => await _userViewModelService.UserAsync(User);

        [HttpPut(nameof(Update))]
        public async Task Update(EditUserViewModel model) => await _userViewModelService.UpdateAsync(User, model);
    }
}