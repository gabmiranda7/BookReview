using BookReview.Models;

namespace BookReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory (int categoryId);
        ICollection<Book> GetBooksByCategory(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
        bool CategoryExists(int categoryId);
    }
}