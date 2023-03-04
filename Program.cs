using Bitirme_Projesi.Entities;
using Bitirme_Projesi.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStr"));
});
builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(opts =>
	{
		opts.Cookie.Name = ".BitirmeProjesi.auth";
		opts.ExpireTimeSpan = TimeSpan.FromDays(7);
		opts.SlidingExpiration = false;
		opts.LoginPath = "/Accounts/Login";
		opts.LogoutPath = "/Accounts/Logout";
		opts.AccessDeniedPath = "/Home/AccessDenied";
	});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});
var app = builder.Build();
SeedDatabase();
 void SeedDatabase()
{
	using (var scope = app.Services.CreateScope())
		try
		{
			var scopedContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			DBSeeder.Seed(scopedContext);
		}
		catch
		{
			throw;
		}
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
//var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
//SeedData.SeedDatabase(context);
app.Run();
