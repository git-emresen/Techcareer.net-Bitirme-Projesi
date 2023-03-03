using Microsoft.AspNetCore.Mvc;
using Bitirme_Projesi.Models;
using Bitirme_Projesi.Entities;

namespace Bitirme_Projesi.Infrastructure.Components
{
    public class SmallCartViewComponent : ViewComponent
        {
                public IViewComponentResult Invoke()
                {
                        List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
                        SmallCartViewModel smallCartVM;

                        if (cart == null || cart.Count == 0)
                        {
                                smallCartVM = null;
                        }
                        else
                        {
                                smallCartVM = new()
                                {
                                        NumberOfItems = cart.Sum(x => x.Quantity),
                                        TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                                };
                        }

                        return View(smallCartVM);
                }
        }
}
