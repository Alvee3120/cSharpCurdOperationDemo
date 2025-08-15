using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupabaseCrudMvc.Models;

namespace SupabaseCrudMvc.Data
{
    public class SupabaseCrudMvcContext : DbContext
    {
        public SupabaseCrudMvcContext (DbContextOptions<SupabaseCrudMvcContext> options)
            : base(options)
        {
        }

        public DbSet<SupabaseCrudMvc.Models.Book> Book { get; set; } = default!;
        public DbSet<SupabaseCrudMvc.Models.Shop> Shop { get; set; } = default!;
        public DbSet<SupabaseCrudMvc.Models.Customer> Customer { get; set; } = default!;
        public DbSet<SupabaseCrudMvc.Models.BookShop> BookShop { get; set; } = default!;
        public DbSet<SupabaseCrudMvc.Models.Order> Order { get; set; } = default!;
    }
}
