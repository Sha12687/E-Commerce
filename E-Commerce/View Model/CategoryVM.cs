using System.ComponentModel.DataAnnotations;

namespace E_Commerce.View_Model
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
