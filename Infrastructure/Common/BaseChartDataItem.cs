using Infrastructure;

namespace Infrastructure
{
    public class BaseChartDataItem : ICalculatePercentTotal
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public decimal Value { get; set; }
        public decimal? Total { get; set; }
        public decimal PercentTotal { get; set; }
        public string Color { get; set; }
    }
}
