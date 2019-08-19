﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<User> LoginAsync(string userName, string password, bool rememberMe)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new RutrackerException("Not valid user name.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new RutrackerException("Not valid password.", ExceptionEventType.NotValidParameters);
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new RutrackerException("User not found.", ExceptionEventType.NotFound);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, rememberMe);

                return user;
            }

            if (result.IsLockedOut)
            {
                throw new RutrackerException("Too many login attempts, try again later.", ExceptionEventType.NotValidParameters);
            }

            if (result.IsNotAllowed)
            {
                throw new RutrackerException("This user is not allowed to log in.", ExceptionEventType.NotValidParameters);
            }

            if (result.RequiresTwoFactor)
            {
                // RequiresTwoFactor
            }

            throw new RutrackerException("Not valid password.", ExceptionEventType.NotValidParameters);
        }

        public async Task<User> RegisterAsync(string userName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new RutrackerException("Not valid user name.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new RutrackerException("Not valid email.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new RutrackerException("Not valid password.", ExceptionEventType.NotValidParameters);
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                throw new RutrackerException("A user with this name is already.", ExceptionEventType.NotValidParameters);
            }

            user = new User
            {
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            result = await _userManager.AddToRoleAsync(user, UserRoles.Names.User);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}