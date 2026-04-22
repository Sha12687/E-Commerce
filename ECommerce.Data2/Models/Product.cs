using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Data2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }


        [Required, StringLength(150)]
       
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}