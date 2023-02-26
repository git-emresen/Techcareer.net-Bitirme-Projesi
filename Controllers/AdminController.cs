using Bitirme_Projesi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Bitirme_Projesi.Controllers
{
	public class AdminController : Controller
	{
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public AdminController(ApplicationDbContext context,IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
             
        }
        public IActionResult Index()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Save(Product product, IFormFile image)
		{
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");





            //TODO:PRODUCT NAME VE CATEGORY NAME KONTROL ET VARSA KAYDI ENGELLE
            var searchProduct = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    //ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    //ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return RedirectToAction(nameof(Index));
                }
                if (image != null)
                {
                    var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }
                else
                {
                    product.Image = "Images/no_image.PNG";
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
               TempData["SuccessMessage"] = "Yeni kayıt eklendi!";
                return RedirectToAction(nameof(Index));
		}
        [HttpPost]
        public IActionResult AddNewCategory(Category category)
        {
            var searchCategory = _context.Categories.FirstOrDefault(p => p.CategoryName == category.CategoryName);
            if (searchCategory != null)
            {
                TempData["CategoryError"] = "Bu kategori zaten mevcut!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
             _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)) ;
            }
          

        }
	}
}
