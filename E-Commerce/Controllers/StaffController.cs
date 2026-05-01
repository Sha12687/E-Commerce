using E_Commerce.View_Model;
using ECommerce.Business;
using ECommerce.Business.Models;
using ECommerce.Business.Service;
using ECommerce.Data2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize(Roles="Admin")]
  public class StaffController : Controller
    {
        private readonly IApplicationUserService _applicationUserService;   
        private readonly UserManager<ApplicationUser> _userManager;
        public StaffController(UserManager<ApplicationUser> userManager, IApplicationUserService applicationUserService)
        {
            _userManager = userManager;
            _applicationUserService = applicationUserService;
        }
        public async Task<IActionResult> Index()
        {
            var staff = (await _userManager.GetUsersInRoleAsync("Staff"))
        .Select(u => new StaffListVM
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            PhoneNumber =u.PhoneNumber
        }).ToList();

            return View(staff);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            StaffVM? staffVM = await _applicationUserService.GetByIdAsync(Id)  ;
            if (staffVM == null)
            {
                return NotFound(); // or RedirectToAction("Index");
            }

            return View(staffVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StaffVM staffVM)
        {
            if (!ModelState.IsValid)
            {
                return View(staffVM);
            }

           await _applicationUserService.UpdateProfileAsync(staffVM, staffVM.Id!);
            return RedirectToAction("Index", "Staff");
        }

        public async Task<IActionResult> Create()
        {
            StaffVM staffVM = new StaffVM();
            return View(staffVM);
        }

        public new async  Task<IActionResult> View(string id)
        {
            var staff = await _applicationUserService.GetByIdAsyncView(id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }


      
        public async Task<IActionResult> Delete(string id)
        {
          await _applicationUserService.DeleteStaffAsync(id);
            TempData["Success"] = "Staff Deleted successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost] 
        public async Task<IActionResult> Create(StaffVM staffVM )
        {
          await  _applicationUserService.CreateStaffAsync(staffVM);
            return RedirectToAction("Index", "Staff");
        }
    }
}
