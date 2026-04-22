using E_Commerce.Model;
using ECommerce.Business.Repository;
using ECommerce.Business.Service;
using ECommerce.Data2.Models;
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
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});

// ✅ MVC ONLY (no default Identity UI)
builder.Services.AddControllersWithViews();

// ❌ REMOVE RazorPages if not using Identity UI
// builder.Services.AddRazorPages();

// ✅ Your custom services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();

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

// ✅ Routing (Controller-based)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ❌ REMOVE this (causes Identity UI conflicts)
// app.MapRazorPages();

app.Run();