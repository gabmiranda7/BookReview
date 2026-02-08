using BookReview.Models;

namespace BookReview.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetAllBooks();
        Book GetBook(int bookId);
        Book GetBookByTitle(string title);
        decimal GetBookRating(int bookId);
        bool BookExists(int bookId);
    }
}
