using BookReview.DTOs;
using BookReview.Interfaces;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviews()
        {
            var reviews = _reviewRepository.GetReviews().Select(r => new ReviewDto
            {
                Id = r.Id,
                Title = r.Title,
                Text = r.Text,
                Rating = r.Rating,
            });
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return BadRequest("Aavliação não encontrada.");
            var review = _reviewRepository.GetReview(reviewId);
            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                Title = review.Title,
                Text = review.Text,
                Rating = review.Rating,
            };
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(review);
        }

        [HttpGet("book/{bookId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfBook(int bookId)
        {
            var reviews = _reviewRepository.GetReviewsOfBook(bookId).Select(r => new ReviewDto
            {
                Id = r.Id,
                Title = r.Title,
                Text = r.Text,
                Rating = r.Rating,
            });
            if (reviews == null)
                return NotFound("Livro não encontrado ou não há avaliações.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);
        }
    }
}