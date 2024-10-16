using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Scion.Caching;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Localizations;

namespace Scion.Data.CommonLocalizations
{
    public class TranslationService : ITranslationService
    {
        private readonly IEnumerable<ITranslationDataProvider> _providers;
        private readonly IPlatformMemoryCache _memoryCache;
        private readonly TranslationOptions _options;

        public TranslationService(IEnumerable<ITranslationDataProvider> providers, IPlatformMemoryCache memoryCache, IOptions<TranslationOptions> options)
        {
            _providers = providers;
            _memoryCache = memoryCache;
            _options = options.Value;
        }

        public JObject GetTranslationDataForLanguage(string lang = null)
        {
            //read fallback localization json object first
            var fallbackCacheKey = CacheKey.With(GetType(), "FallbackJson");
            var result = _memoryCache.GetOrCreateExclusive(fallbackCacheKey, cacheEntry =>
            {
                //Add cache  expiration token
                cacheEntry.AddExpirationToken(LocalizationCache.CreateChangeToken());
                return InnerGetTranslationData(_options.FallbackLanguage);
            });

            if (lang != null && !lang.EqualsInvariant(_options.FallbackLanguage))
            {
                var cacheKey = CacheKey.With(GetType(), "RequestedLanguageJson", lang);
                result = _memoryCache.GetOrCreateExclusive(cacheKey, cacheEntry =>
                {
                    //Add cache  expiration token
                    cacheEntry.AddExpirationToken(LocalizationCache.CreateChangeToken());
                    var langJson = InnerGetTranslationData(lang);
                    // Make another instance of fallback language to avoid corruption of it in the memory cache
                    result = (JObject)result.DeepClone();
                    // Need merge default and requested localization json to resulting object
                    result.Merge(langJson, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Merge });
                    return result;
                });
            }
            return result;
        }

        public string[] GetListOfInstalledLanguages()
        {
            var cachekey = CacheKey.With(GetType(), nameof(GetListOfInstalledLanguages));
            var result = _memoryCache.GetOrCreateExclusive(cachekey, cacheEntry =>
            {
                cacheEntry.AddExpirationToken(LocalizationCache.CreateChangeToken());
                return _providers.SelectMany(x => x.GetListOfInstalledLanguages()).Distinct().ToArray();
            });
            return result;
        }


        protected JObject InnerGetTranslationData(string language)
        {
            //Add cache  expiration token
            JObject result = null;
            foreach (var langJson in _providers.Select(x => x.GetTranslationDataForLanguage(language)))
            {
                if (result == null)
                {
                    result = langJson;
                }
                else
                {
                    result.Merge(langJson, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Merge });
                }
            }
            return result;
        }


    }
}
