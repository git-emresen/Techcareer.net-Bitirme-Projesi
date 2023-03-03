using Bitirme_Projesi.Entities;
using Bitirme_Projesi.Infrastructure;
using Bitirme_Projesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitirme_Projesi.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult GoShopping(int Id)
        {
            var item = _context.UserLists.FirstOrDefault(c => c.Id == Id);
            return RedirectToAction("ShowCart"); 
        }

        public IActionResult ShowCart()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();


            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(cartVM);
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("ProductId","UnitsInStock")] Product p)
        {
            
            Product product = await _context.Products.FindAsync(p.ProductId);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ProductId == p.ProductId).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
                CartItem cItem = cart.Where(c => c.ProductId == p.ProductId).FirstOrDefault();
                cItem.Quantity = p.UnitsInStock;
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "Ürün eklendi!";

            return Redirect(Request.Headers["Referer"].ToString());

        }
        public async Task<IActionResult> Decrease(long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "Ürün kaldırıldı!";

            return RedirectToAction("ShowCart");
        }

        public async Task<IActionResult> Remove(long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "Ürün kaldırıldı!";

            return RedirectToAction("ShowCart");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("ShowCart");
        }
    }
}
