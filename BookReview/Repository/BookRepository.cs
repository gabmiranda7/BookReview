using BookReview.Data;
using BookReview.Interfaces;
using BookReview.Models;
namespace BookReview.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool BookExists(int bookId)
        {
            return _context.Books.Any(b => b.Id == bookId);
        }

        public ICollection<Book> GetAllBooks()
        {
            return _context.Books.OrderBy(b => b.Id).ToList();
        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(b => b.Id == id).FirstOrDefault();
        }

        public Book GetBookByTitle(string title)
        {
            return _context.Books.Where(b => b.Title.Trim().ToUpper() == title.Trim().ToUpper()).FirstOrDefault();
        }

        public decimal GetBookRating(int bookId)
        {
            var reviews = _context.Reviews.Where(r => r.Book.Id == bookId);
            if (!reviews.Any())
                return 0;
            
            return(decimal)reviews.Average(r => r.Rating);
        }
    }
}
