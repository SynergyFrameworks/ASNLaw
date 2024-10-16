using System;

namespace Infrastructure.Common.Domain.Performance
{
    public class DataSyncUpdate
    {
        public bool Updated { get; set; }
        public string OldVal { get; set; }
        public string NewVal { get; set; }
        public string Property { get; set; }

        public T CheckValue<T>(T oldVal, T newVal, string property)
        {
            if (newVal == null)
                return oldVal;
            OldVal = oldVal == null ? null : oldVal.ToString();
            NewVal = newVal.ToString();
            if (OldVal == NewVal)
                return oldVal;

            Property = property;
            Updated = true;
            return newVal;
        }

        public T CheckCodeDescription<T>(T oldVal, T newVal, string property) where T : CodeDescription
        {
            if (newVal == null || string.IsNullOrEmpty(newVal.Code))
                return oldVal;
            if (oldVal == null)
                oldVal = Activator.CreateInstance<T>();
            OldVal = oldVal.Code;
            NewVal = newVal.Code;
            if (OldVal == NewVal)
                return oldVal;

            Property = property;
            Updated = true;
            return oldVal.Code == newVal.Code ? oldVal : newVal;
        }
    }
}
