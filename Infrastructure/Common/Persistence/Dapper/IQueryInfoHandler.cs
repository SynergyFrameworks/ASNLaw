namespace Infrastructure.Common.Persistence.Dapper
{
    public interface IQueryInfoHandler
    {
        void OnQuery<T>(object sender, QueryInfo queryInfo);
    }
}
