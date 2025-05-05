using LagerSystem.Shared.Data;
using LagerSystem.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem.Shared.Services
{
    public class DatabaseUserService
    {
        private readonly AppDbContext _context;

        public DatabaseUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserSession> LoginAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Name == username);

            if (user == null)
                return null;

            var role = user.UserRoles.FirstOrDefault()?.Role?.RoleName ?? "Ukendt";
            return new UserSession { Username = user.Name, Role = role };
        }
    }
}
