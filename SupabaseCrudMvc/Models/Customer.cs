using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupabaseCrudMvc.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Phone, StringLength(50)]
        public string? Phone { get; set; }

        // Navigation
        public ICollection<Order>? Orders { get; set; }
    }
}
