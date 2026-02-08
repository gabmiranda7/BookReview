using BookReview.Models;

namespace BookReview.Interfaces
{
    public interface IAuthorRepository
    {
        ICollection<Author> GetAllAuthors();
        Author GetAuthorById (int authorId);
        ICollection<Author> GetAuthorsOfBooks(int bookId);
        ICollection<Book> GetBooksByAuthors(int authorId);
        bool CreateAuthor(Author author);
        bool UpdateAuthor(Author author);
        bool DeleteAuthor(Author author);
        bool Save();
        bool AuthorExists(int authorId);
    }
}