using Lageret.Services;
using Lageret.Shared.Data;
using Lageret.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.Versioning;

namespace Lageret
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

            // Add device-specific services used by the LoginTest.Shared project  
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LagerSystemDB;Trusted_Connection=True;TrustServerCertificate=True;"));

            builder.Services.AddScoped<DatabaseUserService>();
            builder.Services.AddScoped<SessionService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<DashboardService>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
