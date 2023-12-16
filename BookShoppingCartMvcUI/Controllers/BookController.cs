using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookShoppingCartMvcUI.Data;
using BookShoppingCartMvcUI.Models;
using BookShoppingCartMvcUI.Repositories;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private readonly IBookReponsitory _bookRepository;
        private readonly ApplicationDbContext _db;

        public BooksController(IBookReponsitory bookRepository, ApplicationDbContext db)
        {
            _bookRepository = bookRepository;
            _db = db;
        }

        [AllowAnonymous]
        // GET: Books
        //public async Task<IActionResult> Index()
        //{
        //    var books = await _bookRepository.GetAllBooksAsync();
        //    return View(books);
        //}

        public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
        {
            IEnumerable<Book> books = await _bookRepository.GetAllBooksAsync(sterm, genreId);
            IEnumerable<Genre> genres = await _bookRepository.Genres();
            BookDisplayModel bookModel = new BookDisplayModel
            {
                Books = books,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId
            };
            return View(bookModel);
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookName,AuthorName,Price,Image")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.CreateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepository.GetBookByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,AuthorName,Price,Image")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookRepository.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepository.GetBookByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
