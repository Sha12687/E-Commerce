using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }

        
        public string? Status { get; set; } // Pending, Completed

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("UserId")]
        public Customer User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
