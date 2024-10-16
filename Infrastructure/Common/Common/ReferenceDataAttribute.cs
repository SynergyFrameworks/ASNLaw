using System;

namespace Infrastructure.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ReferenceDataAttribute: Attribute
    {
        public Type RefDataType { get; set; }
        public ReferenceDataAttribute(Type type)
        {
            RefDataType = type;
        }
    }
}
