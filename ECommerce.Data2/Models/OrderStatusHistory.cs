using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{

    public class OrderStatusHistory
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; }  // Placed, Shipped, Delivered, Cancelled

        [Required]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
