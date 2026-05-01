using E_Commerce.Data2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Models
{
    public class StaffVM
    {
        public string? Id { get; set; } 
        public string? FullName { get; set; }
        public string? Email { get; set; }


        public string? Password   { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public string? ExistingImage { get; set; }
        public IFormFile? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }

        public string? Country { get; set; }
        public string? PinCode { get; set; }
        public string? State { get; set; }

        public ICollection<FeedbackVM>? FeedbackVMs { get; set; }
    }
}
