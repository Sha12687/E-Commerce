using E_Commerce.Data2.Models;
using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
