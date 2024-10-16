namespace Infrastructure.Common.Assets
{
    public interface IBlobUrlResolver
    {
        string GetAbsoluteUrl(string blobKey);
    }
}
