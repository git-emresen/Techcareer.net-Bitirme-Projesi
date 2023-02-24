using Microsoft.AspNetCore.Mvc;

namespace Bitirme_Projesi.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Admin()
		{
			return View();
		}
	}
}
