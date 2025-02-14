using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Scion.Infrastructure;
using Scion.Caching;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Domain.Reference;

namespace Scion.Data.Common
{
    public class FileSystemCountriesService : ICountriesService
    {
        private readonly PlatformOptions _platformOptions;
        private readonly IPlatformMemoryCache _memoryCache;

        public FileSystemCountriesService(IOptions<PlatformOptions> platformOptions, IPlatformMemoryCache memoryCache)
        {
            _platformOptions = platformOptions.Value;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get Full list of countries defined by ISO 3166-1 alpha-3
        /// based on https://en.wikipedia.org/wiki/ISO_3166-1_alpha-3
        /// </summary>
        public Task<IList<Country>> GetCountriesAsync()
        {
            var cacheKey = CacheKey.With(GetType(), nameof(GetCountriesAsync));
            return _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                var filePath = Path.GetFullPath(_platformOptions.CountriesFilePath);
                return JsonConvert.DeserializeObject<IList<Country>>(await File.ReadAllTextAsync(filePath));
            });
        }

        public async Task<IList<CountryRegion>> GetCountryRegionsAsync(string countryId)
        {
            var cacheKey = CacheKey.With(GetType(), nameof(GetCountryRegionsAsync));
            var countries = await _memoryCache.GetOrCreateExclusive(cacheKey, async (cacheEntry) =>
             {
                 var filePath = Path.GetFullPath(_platformOptions.CountryRegionsFilePath);
                 return JsonConvert.DeserializeObject<IList<Country>>(await File.ReadAllTextAsync(filePath));
             });

            var country = countries.FirstOrDefault(x => x.Id.Equals(countryId, StringComparison.InvariantCultureIgnoreCase));
            if (country != null)
            {
                return country.Regions;
            }

            return Array.Empty<CountryRegion>();
        }
    }
}
