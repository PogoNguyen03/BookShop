using BookShoppingCartMvcUI.Models;
using BookShoppingCartMvcUI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;
        private readonly ApplicationDbContext _db;


        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository, ApplicationDbContext db)
        {
            _homeRepository = homeRepository;
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index(string sterm="",int genreId=0)
        {
            IEnumerable<Book> books = await _homeRepository.GetBooks(sterm, genreId);
            IEnumerable<Genre> genres = await _homeRepository.Genres();
            BookDisplayModel bookModel = new BookDisplayModel
            {
              Books=books,
              Genres=genres,
              STerm=sterm,
              GenreId=genreId
            };
            return View(bookModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books.Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            // Nếu book không null, thiết lập giá trị cho GenreName
            if (book != null)
            {
                book.GenreName = book.Genre?.GenreName ?? "Không có thể loại";
            }
            return View(book);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}