using BookReview.Models;

namespace BookReview.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByAuthor(int authorId);
        bool CountryExists(int countryId);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        ICollection<Author> GetAuthorsFromCountry(int countryId);
    }
}