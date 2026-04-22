
using ECommerce.Data2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Data2.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

      
        public string? UserId { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? state { get; set; }
        public string? Pincode { get; set; }
        public string? AddressLine { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

    }
}
