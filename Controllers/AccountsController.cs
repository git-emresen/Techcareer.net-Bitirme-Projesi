using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bitirme_Projesi.Data;
using Bitirme_Projesi.Models.Sessions;

namespace Bitirme_Projesi.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Account.ToListAsync());
            return View();
        }


        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var checkUser = _context.Account.Select(user => new
            {
                user.Email,
                user.Password
            }).Where(u => u.Email == email && u.Password == password);

            if (checkUser.Count() != 0)
            {
                HttpContext.Session.SetString("username", email);
                return RedirectToAction("Welcome", "Accounts");
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

            return RedirectToAction("Index", "Accounts");
        }


        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Email == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,PasswordRepeat")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

    }  
       
}
