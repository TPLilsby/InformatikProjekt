using Lageret.Shared.Data;
using Lageret.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lageret.Shared.Services
{

    public class DashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderEntry>> GetRecentOrdersAsync(int count)
        {
            return await _context.OrderEntries
                .Include(o => o.Product)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .Take(count)
                .ToListAsync();
        }

    }
}
