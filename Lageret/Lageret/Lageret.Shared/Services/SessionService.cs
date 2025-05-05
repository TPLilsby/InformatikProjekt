using Lageret.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lageret.Shared.Services
{
    public class SessionService
    {
        private UserSession _currentUser;
        private readonly DatabaseUserService _userService;

        public event Action OnSessionChanged;

        public SessionService(DatabaseUserService userService)
        {
            _userService = userService;
        }

        public bool IsLoggedIn => _currentUser != null;
        public string Role => _currentUser?.Role;
        public string Username => _currentUser?.Username;

        public async Task<bool> LoginAsync(string username, string password)
        {
            var session = await _userService.LoginAsync(username, password);
            if (session != null)
            {
                _currentUser = session;
                NotifyStateChanged();
                return true;
            }

            return false;
        }

        public void Logout()
        {
            _currentUser = null;
            NotifyStateChanged();
        }

        public UserSession GetUser() => _currentUser;

        private void NotifyStateChanged() => OnSessionChanged?.Invoke();
    }
}