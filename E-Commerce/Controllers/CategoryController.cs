using E_Commerce.Models;
using E_Commerce.View_Model;
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
            var vmList= categories.Select(x=> new CategoryVM
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
    }
}
