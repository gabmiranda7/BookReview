using BookReview.Data;
using BookReview.Interfaces;
using BookReview.Models;

namespace BookReview.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly AppDbContext _context;
        public ReviewerRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public Reviewer GetReviewers(int reviewerId)
        {
            return _context.Reviewers.FirstOrDefault(r => r.Id == reviewerId);
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }
    }
}