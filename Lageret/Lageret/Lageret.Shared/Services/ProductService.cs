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
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var existing = await _context.Products.FindAsync(product.ProductId);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Quantity = product.Quantity;
                existing.StockLimit = product.StockLimit;
                existing.CategoryId = product.CategoryId;
                existing.Price = product.Price;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task ReorderAsync(int productId, int userId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null && product.Quantity < product.StockLimit)
            {
                product.Quantity += 10; // fast genbestilling, fx +10 stk

                _context.OrderEntries.Add(new OrderEntry
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 10,
                    OrderDate = DateTime.Now
                });

                await _context.SaveChangesAsync();
            }
        }
    }

}
