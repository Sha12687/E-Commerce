using E_Commerce.Data2.Models;
using E_Commerce.Generic;
using E_Commerce.View_Model;
using ECommerce.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace E_Commerce.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService service,
                                 ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }
        //[Area("Admin")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllProducts();

            var vm = products.Select(p => new ProductListVM
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Price = p.Price,
                Stock = p.Quantity
            });

            return View(vm);
        }


        //public async Task<IActionResult> Create()
        //{
        //    ViewBag.Categories = await _categoryService.GetAllCategories();
        //    return View();
        //}

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategories();
            var vs = new ProductFormVM()
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,

                }).ToList()
            };
            return View("ProductForm", vs); ;
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductFormVM product)
        {

            if (!ModelState.IsValid)
            {
                return View("ProductForm", product);
            }
            if (product.ImageFile != null)
            {

                await SaveImageClass.SaveImage(product.ImageFile);
            }


            await _service.CreateProduct(new Product
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Quantity = product.Stock,
                Image = product.ImagePath,
                Description = product.Description,

            });
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
          
            var product = await _service.GetProductById(id);
            var category = await _categoryService.GetAllCategories();
            var vm = new ProductFormVM
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Stock = product.Quantity,
                ImagePath =product.Image,
                Description = product.Description,
                Categories = category.Select(c => new SelectListItem {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                })
            };

            return View("ProductForm", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductFormVM vm)
        {
            if (!ModelState.IsValid)
            { 
                return View("ProductForm", vm);
            }

            var product = await _service.GetProductById(vm.Id);

            if (product == null)
                return NotFound();

            product.Name = vm.Name;
            product.Price = vm.Price;
            product.Quantity = vm.Stock;
            product.CategoryId = vm.CategoryId;
            product.Description = vm.Description;
            if (vm.ImageFile != null)
            {
                product.Image = await SaveImageClass.SaveImage(vm.ImageFile);
            }

            await _service.UpdateProduct(product);
            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProduct(id);
            TempData["Success"] = "Product Deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
