namespace BookShoppingCartMvcUI.Repositories
{
    public interface IBookReponsitory
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(string sTerm = "", int genreId = 0);
        Task<Book> GetBookByIdAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        bool BookExists(int id);
        Task<IEnumerable<Genre>> Genres();
    }
}
