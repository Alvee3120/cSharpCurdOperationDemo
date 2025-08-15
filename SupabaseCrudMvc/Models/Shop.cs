using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupabaseCrudMvc.Models
{
    public class Shop
    {
        public int ShopId { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Location { get; set; }

        [EmailAddress, StringLength(150)]
        public string? ContactEmail { get; set; }

        // Navigation
        public ICollection<BookShop>? BookShops { get; set; }
    }
}
