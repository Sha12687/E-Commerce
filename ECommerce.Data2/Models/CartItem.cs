using E_Commerce.Data2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Data2.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Navigation
        [ForeignKey("CartId")]
       
        public Cart? Cart { get; set; }
        

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}