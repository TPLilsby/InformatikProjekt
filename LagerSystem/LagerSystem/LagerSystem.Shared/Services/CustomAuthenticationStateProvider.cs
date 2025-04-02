using LagerSystem.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace LagerSystem.Shared.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private User _currentUser;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_currentUser == null)
            {
                _currentUser = await GetUserFromStorage();
            }

            var identity = _currentUser == null
                ? new ClaimsIdentity()
                : new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, _currentUser.UserName),
                    new Claim(ClaimTypes.Role, _currentUser.Role)
                }, "Custom");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Login(User user)
        {
            _currentUser = user;
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authUser", JsonSerializer.Serialize(user));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            _currentUser = null;
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authUser");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public bool IsUserAuthenticated => _currentUser != null;

        public string GetUserRole() => _currentUser?.Role ?? "";

        private async Task<User> GetUserFromStorage()
        {
            var userJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authUser");
            return userJson != null ? JsonSerializer.Deserialize<User>(userJson) : null;
        }
    }
}
