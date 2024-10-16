namespace Infrastructure.Security
{
    public interface IUserNameResolver
    {
        string GetCurrentUserName();
        void SetCurrentUserName(string userName);
    }
}
