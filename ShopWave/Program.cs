using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
string connectionString = configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDBContext>(c => c.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddLazyCache();

builder.Services.AddSession();




builder.Services.AddAuthentication()
	.AddGoogle(options =>
	{
		options.ClientId = "798726742753-kc46gaemmk3gu970juhv8bcg486i6ant.apps.googleusercontent.com";
		options.ClientSecret = "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";
	});

builder.Services.Configure<RazorViewEngineOptions>(o =>
{
	o.ViewLocationFormats.Clear();
	o.ViewLocationFormats.Add("/Pages/HomePage/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Documentation/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/SellerHub/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/AdminHub/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/CartPage/Views/{0}" + RazorViewEngine.ViewExtension);
	o.ViewLocationFormats.Add("/Pages/AccountPage/Views/{0}" + RazorViewEngine.ViewExtension);
	o.ViewLocationFormats.Add("/Pages/AuthorizePage/Views/{0}" + RazorViewEngine.ViewExtension);
	o.ViewLocationFormats.Add("/Pages/SupportPage/Views/{0}" + RazorViewEngine.ViewExtension);
	o.ViewLocationFormats.Add("/Pages/ProductPage/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/OrderPage/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Shared/{0}" + RazorViewEngine.ViewExtension);
	o.ViewLocationFormats.Add("/Pages/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
});



var app = builder.Build();

app.UseSession();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "account",
	pattern: "{action}",
	defaults: new { Controller = "Account" });

app.MapControllerRoute(
    name: "documentation",
    pattern: "{action}",
    defaults: new { Controller = "Documentation" });

app.MapControllerRoute(
	name: "authorize",
	pattern: "{action}",
	defaults: new { Controller = "Authorize" });

app.MapControllerRoute(
    name: "support",
    pattern: "{action}",
    defaults: new { Controller = "Support" });

app.MapControllerRoute(
    name: "home",
    pattern: "{action}",
    defaults: new { Controller = "Home" });

app.MapControllerRoute(
    name: "cart",
    pattern: "{action}",
    defaults: new { Controller = "Cart" });

app.MapControllerRoute(
    name: "order",
    pattern: "{action}",
    defaults: new { Controller = "Order" });

app.MapControllerRoute(
	name: "details",
    pattern: "{action}/{id}",
    defaults: new { controller = "Product", action = "details" }
    );

app.MapControllerRoute(
    name: "products",
    pattern: "{action}",
    defaults: new { controller = "Product", action = "products" }
);

app.MapControllerRoute(
        name: "NotFound",
        pattern: "{*url}",
        defaults: new { controller = "Home", action = "NotFound" }
    );



app.Run();
