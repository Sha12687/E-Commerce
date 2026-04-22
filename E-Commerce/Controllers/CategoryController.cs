using E_Commerce.Data2.Models;
using E_Commerce.Models;
using E_Commerce.View_Model;
using ECommerce.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)

        {
            
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
           
            var categories = await _service.GetAllCategories();
            var vmList = categories.Select(x => new CategoryVM
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return View(vmList);
        }

        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = vm.Name
                };

                await _service.CreateCategory(category);
                return RedirectToAction("Index");
            }

            return View(vm);
        }
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetCategoryById(id);

            if (category == null)
                return NotFound();

            var vm = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = new Category
            {
                Id =model.Id,
                Name=model.Name
            };
             _service.UpdateCategory(category);
            TempData["Success"] = "Category updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCategory(id);
            TempData["Success"] = "Category Deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
