namespace Infrastructure.Common.Domain
{
    public interface IHaveUniqueKey
    {
        string GetUniqueKey(bool hasIdKey = false);
    }
}