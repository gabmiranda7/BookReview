namespace BookReview.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}