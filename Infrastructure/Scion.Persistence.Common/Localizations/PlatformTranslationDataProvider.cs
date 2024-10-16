using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Scion.Infrastructure.Localizations;

namespace Scion.Data.CommonLocalizations
{
    public class PlatformTranslationDataProvider : FileSystemTranslationDataProvider
    {
        private readonly string  _path;
        public PlatformTranslationDataProvider(IOptions<TranslationOptions> options)
        {
            _path = options.Value.PlatformTranslationFolderPath;
        }
        protected override IEnumerable<string> DiscoveryFolders
        {
            get
            {
                yield return _path;
            }
        }
    }
}
