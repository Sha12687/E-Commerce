using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.View_Model
{
    public class ProductFormVM
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }

        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }

        // Optional: store saved path
        public string? ImagePath { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
