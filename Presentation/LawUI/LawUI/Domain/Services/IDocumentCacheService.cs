
namespace LawUI.Domain.Services
{
    public interface IDocumentCacheService
    {
        Task<string> GetOrExtractTextAsync(string filePath);
    }
}