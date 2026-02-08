using BookReview.DTOs;
using BookReview.Interfaces;
using BookReview.Models;
using BookReview.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ICountryRepository _countryRepository;
        
        public AuthorsController(IAuthorRepository authorRepository, ICountryRepository countryRepository)
        {
            _authorRepository = authorRepository;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type  = typeof(IEnumerable<AuthorDto>))]
        public IActionResult GetAuthors()
        {
            var authors = _authorRepository.GetAllAuthors().Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                CountryId = a.CountryId
            });
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthor(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
                return BadRequest("Autor não encontrado.");

            var author = _authorRepository.GetAuthorById(authorId);
            var authorDto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                CountryId = author.CountryId
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(authorDto);
        }

        [HttpGet("book/{bookId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorsOfBooks(int bookId)
        {
            var author = _authorRepository.GetAuthorsOfBooks(bookId).Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                CountryId= a.CountryId
            });
            if (author == null)
                return NotFound("Livro não encontrado.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(author);
        }

        [HttpGet("{authorId}/books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetBooksByAuthors(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
                return NotFound("Autor não encontrado.");
            var books = _authorRepository.GetBooksByAuthors(authorId).Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ReleaseDate = b.ReleaseDate
            });
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(books);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateAuthor([FromBody] AuthorDto newAuthor)
        {
            if (newAuthor == null)
                return BadRequest("Dados inválidos.");

            if (!_countryRepository.CountryExists(newAuthor.CountryId))
                return BadRequest("CountryId inválido.");

            var existingAuthor = _authorRepository.GetAllAuthors()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == newAuthor.Name.Trim().ToUpper());
            if (existingAuthor != null)
            {
                ModelState.AddModelError("", "Autor já existente.");
                return StatusCode(422, ModelState);
            }

            var authorToCreate = new Author
            {
                Name = newAuthor.Name,
                Bio = newAuthor.Bio,
                CountryId = newAuthor.CountryId
            };

            if (!_authorRepository.CreateAuthor(authorToCreate))
            {
                ModelState.AddModelError("", "Algo de errado aconteceu tentando salvar o autor.");
                return StatusCode(500, ModelState);
            }

            return Ok("Autor adicionado com sucesso.");
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDto updatedAuthor)
        {
            if (updatedAuthor == null || authorId != updatedAuthor.Id)
                return BadRequest("Autor inválido.");

            if (!_authorRepository.AuthorExists(authorId))
                return NotFound("Autor não encontrado.");

            var authorToUpdate = _authorRepository.GetAuthorById(authorId);

            authorToUpdate.Name = updatedAuthor.Name;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _authorRepository.UpdateAuthor(authorToUpdate);

            return NoContent();
        }

        [HttpDelete("{authorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAuthor(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
                return NotFound("Autor não encontrado.");

            var authorToDelete = _authorRepository.GetAuthorById(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_authorRepository.DeleteAuthor(authorToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar o autor.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}