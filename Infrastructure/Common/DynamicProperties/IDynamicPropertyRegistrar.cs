using System.Collections.Generic;

namespace Infrastructure.DynamicProperties
{
    public interface IDynamicPropertyRegistrar
    {
        IEnumerable<string> AllRegisteredTypeNames { get; }
        /// <summary>
        /// Register new type name which can support dynamic properties
        /// </summary>
        void RegisterType<T>() where T : IHasDynamicProperties;
    }
}
