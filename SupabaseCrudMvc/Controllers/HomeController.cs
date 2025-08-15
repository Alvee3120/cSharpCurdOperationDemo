using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupabaseCrudMvc.Data;
using SupabaseCrudMvc.Models;
using System.Diagnostics;

namespace SupabaseCrudMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        // Inject both ILogger and DbContext
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch all books from the database
            var books = _context.Books.ToList();
            return View(books); // Pass to the view
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
