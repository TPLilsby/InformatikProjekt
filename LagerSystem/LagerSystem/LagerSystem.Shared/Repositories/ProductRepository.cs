using LagerSystem.Shared.Data;
using LagerSystem.Shared.Models;
using LagerSystem.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ProductRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChangesAsync();
            }
        }
    }
}
