using LagerSystem.Shared.Data;
using LagerSystem.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LagerSystem.Shared.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            if (await GetUserByUsernameAsync(user.UserName) != null)
                return false; // Brugernavn findes allerede

            user.PasswordHash = HashPassword(user.Password); // Hash password

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] salt = Encoding.UTF8.GetBytes("statisk_salt"); // Burde være unik pr. bruger
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combined = passwordBytes.Concat(salt).ToArray();
                byte[] hashBytes = sha256.ComputeHash(combined);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}
