using LMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class MagazineController : Controller
    {
        // Simple in-memory sample list for magazines (no DB changes)
        private static readonly List<Magazine> _sample = new()
        {
            new Magazine { MagazineId = 1, Title = "Tech Monthly", Publisher = "TechPress", IssueDate = DateTime.UtcNow.AddMonths(-1) },
            new Magazine { MagazineId = 2, Title = "Science World", Publisher = "SciencePub", IssueDate = DateTime.UtcNow.AddMonths(-2) }
        };

        public IActionResult Index()
        {
            return View(_sample);
        }

        public IActionResult Details(int id)
        {
            var item = _sample.FirstOrDefault(m => m.MagazineId == id);
            if (item == null) return View("NotFound");
            return View(item);
        }
    }
}
