using Bitirme_Projesi.Entities;
using Bitirme_Projesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using X.PagedList;

namespace Bitirme_Projesi.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private ApplicationDbContext _context;
		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}
		public IActionResult Index(int page = 1)
		{
			IPagedList<Product> products = _context.Products
				.Select(p => new Product() {
					ProductId=p.ProductId,
					ProductName = p.ProductName,
					ProductDescription=p.ProductDescription,
					UnitPrice=p.UnitPrice,
					Image=p.Image
				}).ToPagedList(page,8);
			return View(products);
          
		}

		public IActionResult Privacy()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult AccessDenied()
		{
			return View();

		}
		[AllowAnonymous]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}