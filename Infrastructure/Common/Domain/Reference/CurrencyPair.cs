using System;


namespace Infrastructure.Common.Domain.Reference
{
    public class CurrencyPair: BasePersistantObject
    {
        public virtual int DisplayOrder { get; set; }
        public virtual Currency SourceCurrency { get; set; }
        public virtual Currency DestinationCurrency { get; set; }
        public virtual string Pair
        {
            get
            {
                return SourceCurrency.Code + "/" + DestinationCurrency.Code;
            }
        }

        public CurrencyPair() { }
        public CurrencyPair(CurrencyPair currencyPair)
        {
            Id = currencyPair.Id;
            IsInactive = currencyPair.IsInactive;
            DisplayOrder = currencyPair.DisplayOrder == null? 0 : (int)currencyPair.DisplayOrder;
           
        }
    }
}
