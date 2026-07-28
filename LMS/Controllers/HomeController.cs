using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;
        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Featured: newest 6 books
            var featured = await _context.Books
                .AsNoTracking()
                .OrderByDescending(b => b.PublishedDate)
                .Take(6)
                .ToListAsync();

            ViewBag.TotalBooks = await _context.Books.CountAsync();
            ViewBag.TotalStudents = await _context.Students!.CountAsync();
            ViewBag.CurrentBorrowed = await _context.BorrowRecords.CountAsync(br => br.ReturnDate == null);

            return View(featured);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Title"] = "About Us";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact Us";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
