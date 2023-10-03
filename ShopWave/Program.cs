using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
string connectionString = configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDBContext>(c => c.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddLazyCache();


builder.Services.Configure<RazorViewEngineOptions>(o =>
{
	o.ViewLocationFormats.Clear();
	o.ViewLocationFormats.Add("/Pages/HomePage/Views/{0}" + RazorViewEngine.ViewExtension);
	o.ViewLocationFormats.Add("/Pages/Shared/{0}" + RazorViewEngine.ViewExtension);
	o.ViewLocationFormats.Add("/Pages/{0}" + RazorViewEngine.ViewExtension);
});




var app = builder.Build();


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

app.Run();
