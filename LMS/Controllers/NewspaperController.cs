using LMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class NewspaperController : Controller
    {
        // Simple in-memory sample list for newspapers (no DB changes)
        private static readonly List<Newspaper> _sample = new()
        {
            new Newspaper { NewspaperId = 1, Title = "Daily News", Publisher = "DailyPub", PublishedDate = DateTime.UtcNow.AddDays(-1) },
            new Newspaper { NewspaperId = 2, Title = "City Herald", Publisher = "HeraldGroup", PublishedDate = DateTime.UtcNow.AddDays(-2) }
        };

        public IActionResult Index()
        {
            return View(_sample);
        }

        public IActionResult Details(int id)
        {
            var item = _sample.FirstOrDefault(n => n.NewspaperId == id);
            if (item == null) return View("NotFound");
            return View(item);
        }
    }
}
