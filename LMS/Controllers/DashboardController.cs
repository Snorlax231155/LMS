using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class DashboardController : Controller
    {
        private readonly LibraryContext _context;
        public DashboardController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardModel();
            model.TotalStudents = await _context.Students!.CountAsync();
            model.TotalBooks = await _context.Books.CountAsync();
            model.TotalLibrarians = await _context.Librarians!.CountAsync();
            model.TotalBorrowings = await _context.BorrowRecords.CountAsync();
            // Currently borrowed = borrow records without a return date
            model.CurrentBorrowed = await _context.BorrowRecords.CountAsync(br => br.ReturnDate == null);
            return View(model);
        }
    }
}
