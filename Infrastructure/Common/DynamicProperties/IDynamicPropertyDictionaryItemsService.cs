using System.Threading.Tasks;

namespace Infrastructure.DynamicProperties
{
    public interface IDynamicPropertyDictionaryItemsService
    {
        Task<DynamicPropertyDictionaryItem[]> GetDynamicPropertyDictionaryItemsAsync(string[] ids);
        Task SaveDictionaryItemsAsync(DynamicPropertyDictionaryItem[] items);
        Task DeleteDictionaryItemsAsync(string[] itemIds);
    }
}
