using ECommerce.Data2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Data2.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}
