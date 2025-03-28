﻿using Microsoft.Extensions.DependencyInjection;
using TuneSpace.Core.Interfaces.IServices;
using TuneSpace.Core.Services;

namespace TuneSpace.Core;

public static class ServiceExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISpotifyService, SpotifyService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IBandService, BandService>();

        return services;
    }
}