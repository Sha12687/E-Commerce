using E_Commerce.Model;
using ECommerce.Business.Repository;
using ECommerce.Business.Service;
using ECommerce.Data2.Models;
using ECommerce.Data2.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ DbContext (ONLY ONE)
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Myconnection")));

// ✅ Identity (ONLY ONCE — using your ApplicationUser)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // optional
})
.AddEntityFrameworkStores<MyContext>()
.AddDefaultTokenProviders();


// ✅ Cookie configuration (important for custom login)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";   // ✅ correct
    options.AccessDeniedPath = "/Account/Login";
});

// ✅ MVC ONLY (no default Identity UI)
builder.Services.AddControllersWithViews();

// ❌ REMOVE RazorPages if not using Identity UI
// builder.Services.AddRazorPages();

// ✅ Your custom services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
var app = builder.Build();

// ✅ Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Order is IMPORTANT
app.UseAuthentication();
app.UseAuthorization();

// ✅ Seed roles/admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedRolesAndAdmin(services);
}

// ✅ Admin shortcut (/Admin)
app.MapControllerRoute(
    name: "admin_shortcut",
    pattern: "Admin",
    defaults: new {controller = "Admin", action = "Index" });

// ✅ Area routing
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

// ✅ Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// ❌ REMOVE this (causes Identity UI conflicts)
// app.MapRazorPages();

app.Run();