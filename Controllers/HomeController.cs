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
		public IActionResult Index(string categorySlug,int page = 1)
		{
            if (categorySlug == "" | categorySlug == null)
            {
            IPagedList<Product> products = _context.Products
                .Select(p => new Product()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    UnitPrice = p.UnitPrice,
                    Image = p.Image
                }).ToPagedList(page, 12);
                return View(products);
            }
            else {
                IPagedList<Product> products = (from p in _context.Products
                                                join c in _context.Categories
                                                on p.CategoryId equals c.CategoryId
                                                where c.CategoryName==categorySlug
                                                select new Product()
                                                {
                                                    ProductId = p.ProductId,
                                                    ProductName = p.ProductName,
                                                    ProductDescription = p.ProductDescription,
                                                    UnitPrice = p.UnitPrice,
                                                    Image = p.Image
                                                }).ToPagedList(page, 12);
                return View(products);
            }      
		}

        [HttpPost]
        public IActionResult Index(string ProductName,int pages=1,int i=0)
        {
            if (ProductName == null)
            {
                return NotFound();
            }
            else
            {
                IPagedList<Product> products = _context.Products
                    .Select(p => new Product()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        ProductDescription = p.ProductDescription,
                        UnitPrice = p.UnitPrice,
                        Image = p.Image
                    })
                    .Where(f => f.ProductName == ProductName)
                    .ToPagedList(pages, 12);

                return View(products);
            }

        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(c => c.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }

		
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();

        }
        public IActionResult Privacy()
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