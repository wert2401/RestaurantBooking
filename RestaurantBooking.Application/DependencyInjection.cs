﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantBooking.Application.Services.Authentication;
using RestaurantBooking.Application.Services.ImagesService;
using RestaurantBooking.Application.Services.RestaurantService;
using RestaurantBooking.Application.Services.RoleService;
using RestaurantBooking.Application.Services.TableService;
using RestaurantBooking.Application.Services.UserService;
using RestaurantBooking.Data;

namespace RestaurantBooking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("sqlite"));
            });

            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            services.Configure<ImageOptions>(configuration.GetSection("Images"));

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}