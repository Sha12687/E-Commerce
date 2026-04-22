using E_Commerce.View_Model;
using ECommerce.Data2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
namespace E_Commerce.Controllers
{
   

    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ViewBag.Error = "Invalid login";
            return View();
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {

            if (!ModelState.IsValid) { 
            
            return View(model);
            }
            if (model.Password !=model.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View();
            }
            var user =  new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber =model.Phone,
            };
            var result = await _userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user == null)
            {
                ViewBag.Message = "If email exists, reset link sent.";
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            ViewBag.Message = $"Reset Token: {token}";
            return View();
        }
    }
}
