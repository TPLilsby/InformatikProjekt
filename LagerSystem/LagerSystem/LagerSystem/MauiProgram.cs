using LagerSystem.Repositories;
using LagerSystem.Services;
using LagerSystem.Shared.Data;
using LagerSystem.Shared.Models;
using LagerSystem.Shared.Repositories;
using LagerSystem.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LagerSystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the LagerSystem.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LagerSystemDB;Trusted_Connection=True;TrustServerCertificate=True;"));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
