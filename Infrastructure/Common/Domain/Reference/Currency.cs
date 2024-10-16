namespace Infrastructure.Common.Domain.Reference
{
    public class Currency : CodeDescription
    { 
        public virtual string ISOCode { get; set; }
        public virtual string Symbol { get; set; }
        public override string Code { get { return ISOCode; } set { ISOCode = value; } }
    }
}
