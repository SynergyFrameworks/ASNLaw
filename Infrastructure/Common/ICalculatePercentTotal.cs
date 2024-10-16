namespace Infrastructure
{
    public interface ICalculatePercentTotal
    {
        decimal Value { get; }
        decimal PercentTotal { get; set; }
    }
}
