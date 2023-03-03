using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bitirme_Projesi.Entities;
using Bitirme_Projesi.Models;
using System.Security.Claims;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;
using Bitirme_Projesi.Infrastructure;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;

namespace Bitirme_Projesi.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [AllowAnonymous]
        public IActionResult Login()
        {
            //return View(await _context.Account.ToListAsync());
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.SingleOrDefault(x => x.Email== ViewModel.Email &&
                x.Password == ViewModel.Password);

                if (user != null)
              {                                     

                  List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
                claims.Add(new Claim(ClaimTypes.Role, user.Role));                
                claims.Add(new Claim("FirstName", user.FirstName));
                claims.Add(new Claim("UserId", user.Id.ToString()));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");

                }
            else
            {
                    ViewBag.Message = "Username or Password is Incorrect"; 
            }
         }
            return View(ViewModel);
        }
		
		public IActionResult Profile()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            var id = identity.FindFirst("UserId");
            
            var lists = _context.UserLists
                .Select(l => new UserList()
                {
                    Id = l.Id,
                    Description = l.Description,
                    IsDone = l.IsDone,
                    UserId = l.UserId,

                })
                .Where(u => u.UserId ==Convert.ToInt32(id.Value));
            return View(lists);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Error()
		{
            ViewBag.ErrorMessage = "Kullanıcı Mevcut!";
            return View();
		}


		// GET: Accounts/Details/5
		public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var account = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([Bind("FirstName,LastName,Email,Password,PasswordRepeat")] RegisterViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(x => x.Email== ViewModel.Email))
                {
                    ModelState.AddModelError(nameof(ViewModel.Email), "Mail adresi zaten kayıtlı");
                    return View();
                }
                
                User user = new User()
                {
                    FirstName = ViewModel.FirstName,
                    LastName = ViewModel.LastName, 
                    Email = ViewModel.Email,
                    Password= ViewModel.Password,
                    PasswordRepeat=ViewModel.PasswordRepeat
                    
                };

                _context.Users.Add(user);
                int effectedrows = _context.SaveChanges();

                if (effectedrows == 0)
                {
                    ModelState.AddModelError("", "User cannot be added");
                }
                else
                {
					return RedirectToAction("Index", "Home");
				}

            }
            return View(ViewModel);


        }

        [HttpPost]
        public IActionResult AddList([Bind("Description","IsDone","UserId")] UserList u)
        {

            string id = HttpContext.User.FindFirstValue("UserId");

            UserList uList = new UserList()
            {
                Description = u.Description,
                IsDone = false,
                UserId = u.UserId
            };

            _context.UserLists.Add(uList);
            int effectedrows = _context.SaveChanges();
            return RedirectToAction("Profile", "Accounts");
        }

        public IActionResult RemoveList(int Id)
        {
            var item = _context.UserLists.Single(c => c.Id == Id);
            _context.UserLists.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Profile", "Accounts");
           

        }
    }  
       
}
