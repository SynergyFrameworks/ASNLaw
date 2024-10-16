using Infrastructure.Common.Domain.Reference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public interface ICountriesService
    {
        Task<IList<Country>> GetCountriesAsync();
        Task<IList<CountryRegion>> GetCountryRegionsAsync(string countryId);
    }
}
