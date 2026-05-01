using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.View_Model
    {
        public class ProductListVM
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? CategoryName { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
        }
    }

