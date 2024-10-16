namespace Infrastructure.Common.Persistence.Dapper
{
    public interface ICriteriaHandler
    {
        void OnQuery<T>(object sender, Criteria criteria);
    }
}
