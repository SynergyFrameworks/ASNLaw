using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Common.Domain.Performance
{
    public class DataSyncCollection
    {
        public List<DataSyncUpdate> UpdateVals { get; set; }

        public DataSyncCollection()
        {
            UpdateVals = new List<DataSyncUpdate>();
        }

        public T CheckValue<T>(T oldVal, T newVal, string property)
        {
            var updateVal = new DataSyncUpdate();
            var result = updateVal.CheckValue(oldVal, newVal, property);
            AddIfUpdated(updateVal);
            
            return result;
        }

        public T CheckCodeDescriptionValue<T>(T oldVal, T newVal, string property) where T : CodeDescription
        {
            var updateVal = new DataSyncUpdate();
            var result = updateVal.CheckCodeDescription(oldVal, newVal, property);
            AddIfUpdated(updateVal);

            return result;
        }

        private void AddIfUpdated(DataSyncUpdate updateVal)
        {
            if (updateVal.Updated)
                UpdateVals.Add(updateVal);
        }

        public bool Updated
        {
            get
            {
                return UpdateVals.Any();
            }
            
        }
    }
}
