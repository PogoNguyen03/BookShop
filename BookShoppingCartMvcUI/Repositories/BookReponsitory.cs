using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repositories
{
    public class BookReponsitory : IBookReponsitory
    {
        private readonly ApplicationDbContext _db;

        public BookReponsitory(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.ToListAsync();
        }

        //public async Task<IEnumerable<Book>> GetAllBooksAsync()
        //{
        //    return await _context.Books.ToListAsync();
        //}

        public async Task<IEnumerable<Book>> GetAllBooksAsync(string sTerm = "", int genreId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Book> books = await (from book in _db.Books
                                             join genre in _db.Genres
                                             on book.GenreId equals genre.Id
                                             where string.IsNullOrWhiteSpace(sTerm) || (book != null && book.BookName.ToLower().StartsWith(sTerm))
                                             select new Book
                                             {
                                                 Id = book.Id,
                                                 Image = book.Image,
                                                 AuthorName = book.AuthorName,
                                                 BookName = book.BookName,
                                                 GenreId = book.GenreId,
                                                 Price = book.Price,
                                                 GenreName = genre.GenreName
                                             }
                         ).ToListAsync();
            if (genreId > 0)
            {

                books = books.Where(a => a.GenreId == genreId).ToList();
            }
            return books;

        }


        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _db.Books.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateBookAsync(Book book)
        {
            _db.Add(book);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _db.Update(book);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }

        public bool BookExists(int id)
        {
            return _db.Books.Any(e => e.Id == id);
        }
    }
}
