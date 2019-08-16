﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Contexts
{
    public class IdentityContextSeed
    {
        public static async Task SeedAsync(
            IdentityContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                if (!await context.Roles.AnyAsync())
                {
                    await SeedRolesAsync(roleManager);
                    await context.SaveChangesAsync();
                }

                if (!await context.Users.AnyAsync())
                {
                    await SeedUsersAsync(userManager);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                loggerFactory.CreateLogger<IdentityContextSeed>().LogError(ex.Message);
            }
        }

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            var roles = new[]
            {
                new Role
                {
                    Name = UserRoles.Names.User
                },
                new Role
                {
                    Name = UserRoles.Names.Admin
                }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin"
            };

            await userManager.CreateAsync(admin, "Admin_Password_123");
            await userManager.AddToRolesAsync(admin, new[]
            {
                UserRoles.Names.User,
                UserRoles.Names.Admin
            });
        }
    }
}