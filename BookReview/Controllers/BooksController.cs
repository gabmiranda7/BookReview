using BookReview.Interfaces;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BooksController>))]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetAllBooks();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            return Ok(books);
        }

        [HttpGet("{bookid}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetBook (int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return BadRequest("Livro não encontrado.");

            var book = _bookRepository.GetBook(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }

        [HttpGet("{bookId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetBookRating (int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return BadRequest("Livro não encontrado.");

            var rating = _bookRepository.GetBookRating(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }
    }
}