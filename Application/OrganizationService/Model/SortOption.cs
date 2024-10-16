using Infrastructure.Common.Sorting;


namespace Infrastructure.CQRS.Models
{
    public class SortOption : ISortingOption
    {
        public string ColumnName { get; set; }
        public bool IsColumnOrderDesc { get; set; }
    }
}