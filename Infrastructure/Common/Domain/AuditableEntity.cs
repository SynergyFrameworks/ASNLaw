using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Common.Domain.Contracts;
using Newtonsoft.Json;

namespace Infrastructure.Common
{
    public abstract class AuditableEntity : Entity, IAuditable
    {
        #region IAuditable Members
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        [StringLength(64)]
        public string CreatedBy { get; set; }
        [StringLength(64)]
        public string ModifiedBy { get; set; }
        #endregion

        #region Conditional JSON serialization for properties declared in base type
        [JsonIgnore]
        public virtual bool ShouldSerializeAuditableProperties { get; } = true;
        /// <summary>
        /// https://www.newtonsoft.com/json/help/html/ConditionalProperties.htm
        /// </summary>
        /// <returns></returns>
        public virtual bool ShouldSerializeCreatedDate()
        {
            return ShouldSerializeAuditableProperties;
        }
        public virtual bool ShouldSerializeModifiedDate()
        {
            return ShouldSerializeAuditableProperties;
        }
        public virtual bool ShouldSerializeCreatedBy()
        {
            return ShouldSerializeAuditableProperties;
        }
        public virtual bool ShouldSerializeModifiedBy()
        {
            return ShouldSerializeAuditableProperties;
        }
        #endregion
    }
}
