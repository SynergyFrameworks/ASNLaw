namespace Infrastructure.Security
{
    public interface ICurrentUser
    {
        string UserName { get; set; }
    }
}
