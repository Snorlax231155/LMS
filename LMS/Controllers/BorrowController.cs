using LMS.Models;
using LMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class BorrowController : Controller
    {
        private readonly LibraryContext _context;
        public BorrowController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Borrow
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .AsNoTracking()
                .ToListAsync();
            return View(books);
        }

        // GET: Borrow/Create?bookId=5
        public async Task<IActionResult> Create(int? bookId)
        {
            if (bookId == null) return View("NotFound");
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return View("NotFound");
            if (!book.IsAvailable) return View("NotAvailable");
            var vm = new BorrowViewModel { BookId = book.BookId, BookTitle = book.Title };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BorrowViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var book = await _context.Books.FindAsync(model.BookId);
            if (book == null) return View("NotFound");
            if (!book.IsAvailable) return View("NotAvailable");

            var borrow = new BorrowRecord
            {
                BookId = book.BookId,
                BorrowerName = model.BorrowerName,
                BorrowerEmail = model.BorrowerEmail,
                Phone = model.Phone,
                BorrowDate = DateTime.UtcNow
            };
            book.IsAvailable = false;
            _context.BorrowRecords.Add(borrow);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Successfully borrowed the book: {book.Title}.";
            return RedirectToAction("Index", "Books");
        }

        // GET: Borrow/Return/5
        public async Task<IActionResult> Return(int? borrowRecordId)
        {
            if (borrowRecordId == null) return View("NotFound");
            var record = await _context.BorrowRecords.Include(br => br.Book).FirstOrDefaultAsync(br => br.BorrowRecordId == borrowRecordId);
            if (record == null) return View("NotFound");
            if (record.ReturnDate != null) return View("AlreadyReturned");
            var vm = new ReturnViewModel
            {
                BorrowRecordId = record.BorrowRecordId,
                BookTitle = record.Book?.Title,
                BorrowerName = record.BorrowerName,
                BorrowDate = record.BorrowDate
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(ReturnViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var record = await _context.BorrowRecords.Include(br => br.Book).FirstOrDefaultAsync(br => br.BorrowRecordId == model.BorrowRecordId);
            if (record == null) return View("NotFound");
            if (record.ReturnDate != null) return View("AlreadyReturned");
            record.ReturnDate = DateTime.UtcNow;
            if (record.Book != null) record.Book.IsAvailable = true;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Successfully returned the book: {record.Book?.Title}.";
            return RedirectToAction("Index", "Books");
        }
    }
}
