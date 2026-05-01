using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ECommerce.Business.Models
{
    public class AdminProfileVM
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        public string? ExistingImage { get; set; }
        public IFormFile? Image { get; set; }

        public string? Street { get; set; }
        public string? City { get; set; }

        public string? Country { get; set; }
        public string? PinCode { get; set; }
        public string? State { get; set; }
    }
}
