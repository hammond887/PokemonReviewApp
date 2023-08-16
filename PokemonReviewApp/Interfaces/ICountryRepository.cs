using PokemonReviewApp.Models;
using System.Collections;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        bool CountryExists(int id);
    }
}
