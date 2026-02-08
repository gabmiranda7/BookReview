using BookReview.Data;
using BookReview.Interfaces;
using BookReview.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;

namespace BookReview.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author GetAuthorById(int authorId)
        {
            return _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
        }

        public ICollection<Author> GetAuthorsOfBooks(int bookId)
        {
            return _context.BookAuthors
                .Where(ba => ba.BookId == bookId)
                .Select(ba => ba.Author)
                .ToList();
        }

        public ICollection<Book> GetBooksByAuthors(int authorId)
        {
            return _context.BookAuthors
                .Where(ba => ba.AuthorId == authorId)
                .Select(ba => ba.Book)
                .ToList();
        }

        public bool CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            return Save();
        }

        public bool UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            return Save();
        }

        public bool DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool AuthorExists(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }
    }
}