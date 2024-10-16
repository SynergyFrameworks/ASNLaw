using System.Collections.Generic;
using Infrastructure.Common.Domain.Contracts;

namespace Infrastructure.DynamicProperties
{
    public interface IHasDynamicProperties : IEntity
    {
        string ObjectType { get; }
        ICollection<DynamicObjectProperty> DynamicProperties { get; set; }
    }
}
