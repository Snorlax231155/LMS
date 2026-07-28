using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly LibraryContext _context;
        public LoginController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Verify(LoginModel usr)
        {
            if (usr == null) return View("Index");
            var user = await _context.LoginTab!.FirstOrDefaultAsync(u => u.Username == usr.Username && u.Password == usr.Password);
            if (user != null)
            {
                TempData["message"] = "Login Success";
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.message = "Login Failed";
            return View("Index");
        }
    }
}
