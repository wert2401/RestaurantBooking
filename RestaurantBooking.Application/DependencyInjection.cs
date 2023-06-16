using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantBooking.Application.Services.Authentication;
using RestaurantBooking.Application.Services.FilesService;
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
                options.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("pgSql")).UseProjectables();
            });

            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            services.Configure<Services.FilesService.FileOptions>(configuration.GetSection("FilePaths"));

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}