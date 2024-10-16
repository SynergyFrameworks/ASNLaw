using System;

namespace Infrastructure.Common.Persistence.Azure.Transformers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreProperty : Attribute
    {
    }
}
