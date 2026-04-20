using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Admin
    {
        [Key]
        public int Admin_id { get; set; }
        [Required]
        
        public string AdminName { get; set; }
        [EmailAddress]
        public string Admin_enmail { get; set; }
        public string Admin_password { get; set; }
        public string Admin_image { get; set; }





    }
}
