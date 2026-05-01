using E_Commerce.Data2.Models;
using E_Commerce.View_Model;
using ECommerce.Business.Models;
using ECommerce.Business.Service;
using ECommerce.Data2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{

   
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAdminService _adminService;

        public AdminController(UserManager<ApplicationUser> userManager,
                                 MyContext context,
                                 IWebHostEnvironment env,
                                 IAdminService adminService)
        {
            _userManager = userManager;
            _context = context;
            _adminService = adminService;
            _env = env;
        }

        public async Task<IActionResult> Index(string id)
        {
            AdminProfileVM fullData =  await _adminService.GetAdminProfileById(id);
            return View(fullData);
        }

        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            AdminProfileVM fullData = await _adminService.GetAdminProfileById(user.Id);
            return View(fullData);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(AdminProfileVM vm)
        {
            var user = await _userManager.GetUserAsync(User);

            user.FullName = vm.FullName;
            user.DOB = vm.DOB;

            if (vm.Image != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "images");
                string fileName = Guid.NewGuid() + Path.GetExtension(vm.Image.FileName);
                string path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await vm.Image.CopyToAsync(stream);

                user.ProfileImage = "/images/" + fileName;
            }

            await _userManager.UpdateAsync(user);

            var address = _context.Addresses.FirstOrDefault(a => a.UserId == user.Id);

            if (address == null)
            {
                address = new Address
                {
                    state = vm.Street,
                    City = vm.City,
                    UserId = user.Id
                };
                _context.Add(address);
            }
            else
            {
                address.state = vm.Street;
                address.City = vm.City;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        

        public IActionResult Register()
        {
            return View();
        }

     
    }
}
