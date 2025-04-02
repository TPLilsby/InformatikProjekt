using LagerSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem.Shared.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> RegisterUserAsync(User user);
        Task<User> AuthenticateUserAsync(string username, string password);
    }
}