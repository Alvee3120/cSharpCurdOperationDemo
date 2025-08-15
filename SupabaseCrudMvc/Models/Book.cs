using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupabaseCrudMvc.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string Author { get; set; } = string.Empty;

        [StringLength(20)]
        public string? ISBN { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishedAt { get; set; }

        // Navigation
        public ICollection<BookShop>? BookShops { get; set; }
    }
}
