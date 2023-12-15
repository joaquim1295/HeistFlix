using EisntFlix.Data.Access.DbContext;
using EisntFlix.Data.Access.DbInitializer;
using EisntFlix.Business.Services;
using EisntFlix.Business.Services.IServices;
using EisntFlix.FetchAPI.APIService;
using EisntFlix.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EisntFlix.Business.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Services configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();




//DbContext
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connString));


//Authentication and Authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

//MVC Configuration
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

//Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
 //   endpoints.MapControllerRoute(
 //   name: "Orderby",
 //   pattern: "{area:exists}/{controller:exists}/{action=Orderby}/{name?}");

	endpoints.MapControllerRoute(
    name: "default",
    pattern: "{area=Content}/{controller=Home}/{action=Index}/{id?}");


    endpoints.MapRazorPages();
});

////Seed database
AppDbInitializer.Initialize(app).Wait();

app.Run();
