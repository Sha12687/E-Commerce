
using E_Commerce.Data2.Models;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress] 
        public string? Email { get; set; }

        public string?  PasswordHash { get; set; }
        public string? CustomerImage { get; set; }
                     
        public string? CustomerGender { get; set; }
                     
        public string? CustomerCity { get; set; }
        public string? Phone { get; set; }
                     
        public string? Role { get; set; } // Admin / Customer

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Address>? Addresses { get; set; }
    }
}
