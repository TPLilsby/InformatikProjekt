using Lageret.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Lageret.Shared.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets for each entity
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptProduct> ReceiptProducts { get; set; }

        // Configuration for many-to-many and other relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Quantity)
                    .IsRequired();

                entity.Property(p => p.StockLimit)
                    .IsRequired();

                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)"); // Sikrer korrekt SQL-mapping

                entity.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Many-to-many relationship for UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            
            modelBuilder.Entity<OrderEntry>()
                .HasKey(oe => new { oe.ProductId, oe.UserId, oe.OrderDate });

            modelBuilder.Entity<OrderEntry>()
                .HasOne(oe => oe.Product)
                .WithMany()
                .HasForeignKey(oe => oe.ProductId);

            modelBuilder.Entity<OrderEntry>()
                .HasOne(oe => oe.User)
                .WithMany()
                .HasForeignKey(oe => oe.UserId);
            
            // Receipt - User relationship
            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.Property(r => r.TotalPrice)
                    .HasColumnType("decimal(10,2)"); // Alternativ: .HasPrecision(10, 2)
            });


            // ReceiptProduct - Receipt and Product relationships
            modelBuilder.Entity<ReceiptProduct>()
                .HasKey(rp => new { rp.ReceiptId, rp.ProductId });

            modelBuilder.Entity<ReceiptProduct>()
                .HasOne(rp => rp.Receipt)
                .WithMany()
                .HasForeignKey(rp => rp.ReceiptId);

            modelBuilder.Entity<ReceiptProduct>()
                .HasOne(rp => rp.Product)
                .WithMany()
                .HasForeignKey(rp => rp.ProductId);
        }
    }
}
