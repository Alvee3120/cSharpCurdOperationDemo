using SupabaseCrudMvc.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupabaseCrudMvc.Models
{
    public class BookShop
    {
        public int BookShopId { get; set; }

        // FKs
        [Required]
        public int BookId { get; set; }

        [Required]
        public int ShopId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 0; // stock level

        // Navigation
        public Book? Book { get; set; }
        public Shop? Shop { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
