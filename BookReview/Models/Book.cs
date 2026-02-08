namespace BookReview.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}