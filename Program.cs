using Bitirme_Projesi.Entities;
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
