﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TuneSpace.Core.Entities;
using TuneSpace.Core.Interfaces.IRepositories;
using TuneSpace.Infrastructure.Data;

namespace TuneSpace.Infrastructure.Repositories;

internal class UserRepository(UserManager<User> userManager, TuneSpaceDbContext context) : IUserRepository
{
    async Task<User?> IUserRepository.GetUserById(string id)
    {
        return await userManager.FindByIdAsync(id);
    }

    async Task<User?> IUserRepository.GetUserByEmail(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    async Task<User?> IUserRepository.GetUserByName(string name)
    {
        return await userManager.FindByNameAsync(name);
    }

    async Task<User?> IUserRepository.GetUserByRefreshToken(string refreshToken)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenValidity > DateTime.Now.ToUniversalTime());
    }

    async Task<List<string>> IUserRepository.SearchByName(string name)
    {
        return await userManager.Users
            .Where(u => u.UserName.StartsWith(name))
            .Select(u => u.UserName)
            .Take(5)
            .ToListAsync();
    }

    async Task IUserRepository.InsertUser(User user, string password)
    {
        await userManager.CreateAsync(user, password);
    }

    async Task IUserRepository.UpdateUser(User user)
    {
        await userManager.UpdateAsync(user);
    }
}
