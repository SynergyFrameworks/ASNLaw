using Infrastructure.Common.Persistence.Azure;
using Infrastructure.Common.Persistence.Azure.Transformers;

namespace Infrastructure.Common.Domain
{
    public class PersistentReferenceEntity:PersistentEntity
    {
        private string _rowKey;
        [IgnoreProperty]
        public override string PartitionKey { get; set; }
        [IgnoreProperty]
        public override string RowKey {
            get
            {
                if (string.IsNullOrEmpty(_rowKey))
                    _rowKey = Code;
                return _rowKey;
            }
            set { _rowKey = value; }
        }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual int? DisplayOrder { get; set; }
        public virtual string Type { get; set; }
    }
}
