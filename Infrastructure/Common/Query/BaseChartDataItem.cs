using System;

namespace Infrastructure.Query
{
    public class BaseChartDataItem
    {
        private string _id { get; set; }
        
        //don't want to force Id to be a guid, but Dapper cannot convert Guid to string automatically.
        public string Id
        {
            get
            {
                if (_id == null && GuidId != null)
                    _id = GuidId.ToString();
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Key { get; set; }
        public decimal Value { get; set; }
        public decimal? Total { get; set; }
        public decimal? PercentTotal { get; set; }
        public string Color { get; set; }
        public Guid? GuidId { get; set; }
    }
}
