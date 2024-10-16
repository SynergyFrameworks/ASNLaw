using System;
using System.ComponentModel.DataAnnotations;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.DynamicProperties;

namespace Scion.Data.Common.Model
{
    public class DynamicPropertyNameEntity : AuditableEntity
    {
        [StringLength(64)]
        public string Locale { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public string PropertyId { get; set; }
        public virtual DynamicPropertyEntity Property { get; set; }

        public virtual DynamicPropertyName ToModel(DynamicPropertyName propName)
        {
            if (propName == null)
            {
                throw new ArgumentNullException(nameof(propName));
            }

            propName.Locale = Locale;
            propName.Name = Name;

            return propName;
        }

        public virtual DynamicPropertyNameEntity FromModel(DynamicPropertyName propName)
        {
            if (propName == null)
            {
                throw new ArgumentNullException(nameof(propName));
            }

            Locale = propName.Locale;
            Name = propName.Name;

            return this;
        }

        public virtual void Patch(DynamicPropertyNameEntity target)
        {
            //Nothing to do
        }
    }
}
