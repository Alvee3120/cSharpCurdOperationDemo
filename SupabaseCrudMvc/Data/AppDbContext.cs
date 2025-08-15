using SupabaseCrudMvc.Models;
using Microsoft.EntityFrameworkCore;
using SupabaseCrudMvc.Models;

namespace SupabaseCrudMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<BookShop> BookShops => Set<BookShop>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Book: ISBN unique if present (optional)
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique()
                .HasFilter("\"ISBN\" IS NOT NULL"); // PostgreSQL-ish filter for nullable unique

            // BookShop: unique composite index on BookId + ShopId to avoid duplicates
            modelBuilder.Entity<BookShop>()
                .HasIndex(bs => new { bs.BookId, bs.ShopId })
                .IsUnique();

            // Relationships

            // BookShop: many-to-one to Book
            modelBuilder.Entity<BookShop>()
                .HasOne(bs => bs.Book)
                .WithMany(b => b.BookShops)
                .HasForeignKey(bs => bs.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // BookShop: many-to-one to Shop
            modelBuilder.Entity<BookShop>()
                .HasOne(bs => bs.Shop)
                .WithMany(s => s.BookShops)
                .HasForeignKey(bs => bs.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order -> Customer (many orders per customer)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order -> BookShop (order references the junction table)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.BookShop)
                .WithMany(bs => bs.Orders)
                .HasForeignKey(o => o.BookShopId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure decimal precision for Price & TotalPrice
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
