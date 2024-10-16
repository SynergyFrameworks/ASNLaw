using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;


namespace LawUI.Domain.Services;

public class DocumentCacheService : IDocumentCacheService
{
    private readonly IMemoryCache _cache;
    private readonly IAdvancedTextExtractorService _textExtractor;

    public DocumentCacheService(IMemoryCache cache, IAdvancedTextExtractorService textExtractor)
    {
        _cache = cache;
        _textExtractor = textExtractor;
    }

    public async Task<string> GetOrExtractTextAsync(string filePath)
    {
        string cacheKey = GetCacheKey(filePath);

        if (!_cache.TryGetValue(cacheKey, out string extractedText))
        {
            extractedText = await _textExtractor.ExtractTextAsync(filePath);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                .SetAbsoluteExpiration(TimeSpan.FromHours(2));

            _cache.Set(cacheKey, extractedText, cacheEntryOptions);
        }

        return extractedText;
    }

    private string GetCacheKey(string filePath)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(filePath);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
