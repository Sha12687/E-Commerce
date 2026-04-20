
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string state { get; set; }
        public string Pincode { get; set; }
        public string AddressLine { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public Customer User { get; set; }
    }
}
