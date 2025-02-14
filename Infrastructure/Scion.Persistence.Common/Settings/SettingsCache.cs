using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Extensions.Primitives;
using Scion.Caching;
using Scion.Infrastructure.Settings;

namespace Scion.Data.Common.Settings
{
    public class SettingsCache : CancellableCache<SettingsCache>
    {
        private static readonly ConcurrentDictionary<string, CancellationTokenSource> _settingsRegionTokenLookup = new ConcurrentDictionary<string, CancellationTokenSource>();

        public static IChangeToken CreateChangeToken(ObjectSettingEntry settingEntry)
        {
            if (settingEntry == null)
            {
                throw new ArgumentNullException(nameof(settingEntry));
            }
            var cancellationTokenSource = _settingsRegionTokenLookup.GetOrAdd(settingEntry.GetCacheKey(), new CancellationTokenSource());
            return new CompositeChangeToken(new[] { CreateChangeToken(), new CancellationChangeToken(cancellationTokenSource.Token) });
        }

        public static void ExpireSetting(ObjectSettingEntry settingEntry)
        {
            if (settingEntry != null && _settingsRegionTokenLookup.TryRemove(settingEntry.GetCacheKey(), out var token))
            {
                token.Cancel();
            }
        }
    }
}
