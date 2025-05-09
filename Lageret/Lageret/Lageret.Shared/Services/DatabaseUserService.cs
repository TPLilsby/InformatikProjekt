using Lageret.Shared.Data;
using Lageret.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lageret.Shared.Services
{
    public class DatabaseUserService
    {
        private readonly AppDbContext _context;
        private const string GlobalSalt = "D3rSk@lV4r3#enHemmel1gStr1ng!";

        public DatabaseUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserSession> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Name == username);

            if (user == null)
                return null;

            var hasher = new PasswordHasher<string>();
            var result = hasher.VerifyHashedPassword(null, user.PasswordHash, CombineWithSalt(password));

            if (result == PasswordVerificationResult.Failed)
                return null;

            var role = user.UserRoles.FirstOrDefault()?.Role?.RoleName ?? "Ukendt";
            return new UserSession { Username = user.Name, Role = role };
        }

        public async Task<List<string>> GetRoleNamesAsync()
        {
            return await _context.Roles.Select(r => r.RoleName).ToListAsync();
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
        }


        public async Task<bool> RegisterAsync(string username, string password, string roleName)
        {
            if (await _context.Users.AnyAsync(u => u.Name == username))
                return false;

            if (!IsPasswordValid(password))
                return false;

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
            if (role == null) return false;

            var hasher = new PasswordHasher<string>();
            var hash = hasher.HashPassword(null, CombineWithSalt(password));

            var user = new User
            {
                Name = username,
                PasswordHash = hash,
                UserRoles = new List<UserRole>
                {
                    new UserRole { Role = role }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        private string CombineWithSalt(string password)
        {
            return password + GlobalSalt;
        }

        private bool IsPasswordValid(string password)
        {
            if (password.Length < 12)
                return false;

            bool hasLetter = password.Any(char.IsLetter);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSymbol = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasLetter && hasDigit && hasSymbol;
        }


    }
}