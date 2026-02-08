using BookReview.Data;
using BookReview.Interfaces;
using BookReview.Models;

namespace BookReview.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<Models.Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public Models.Review GetReview(int reviewId)
        {
            return _context.Reviews.FirstOrDefault(r => r.Id == reviewId);
        }

        public ICollection<Models.Review> GetReviewsOfBook(int bookId)
        {
            return _context.Reviews
                .Where(r => r.Book.Id == bookId)
                .ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
    }
}
