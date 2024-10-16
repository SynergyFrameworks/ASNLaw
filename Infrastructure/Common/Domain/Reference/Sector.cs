namespace Infrastructure.Common.Domain.Reference
{
    public class Sector : CodeDescription,IHaveUniqueKey
    {
        public virtual string Color { get; set; }
        public virtual string GetUniqueKey(bool hasIdKey = false)
        {
            return Code;
        }
    }
}
