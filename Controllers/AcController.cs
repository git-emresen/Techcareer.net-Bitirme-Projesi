using Bitirme_Projesi.Models.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme_Projesi.Controllers
{    
    public class AcController : Controller
    {
        private IAccountService _accountService;
        public AcController(IAccountService accountService)
        {
            _accountService= accountService;    
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var account = _accountService.Login(username, password);
            if (account != null)
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Welcome","Account");
            }
            else
            {
                ViewBag.Message = "Invalid Login";
                return View();
            }
           
        }

        public IActionResult Welcome()
        {
            ViewBag.Message = "Sucsess Login";
            ViewBag.Message2 = HttpContext.Session.GetString("username");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");

            return RedirectToAction("Index", "Account");
        }
    }
}
