using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Data2.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PaymentMethod { get; set; }  // UPI, Card, COD

        [Required]
        [MaxLength(30)]
        public string? PaymentStatus { get; set; }  // Pending, Success, Failed

        [Required]
        public DateTime PaidAt { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
    }
}
