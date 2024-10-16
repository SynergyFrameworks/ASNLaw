
namespace LawUI.Domain.Services
{
    public interface IAdvancedTextExtractorService
    {
        Task<string> ExtractTextAsync(string filePath);
    }
}