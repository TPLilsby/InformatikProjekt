using LagerSystem.Repositories;
using LagerSystem.Shared.Data;
using LagerSystem.Shared.Models;
using LagerSystem.Shared.Repositories;
using LagerSystem.Shared.Services;
using LagerSystem.Web.Components;
using LagerSystem.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add device-specific services used by the LagerSystem.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LagerSystemDB;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(LagerSystem.Shared._Imports).Assembly);

app.Run();
