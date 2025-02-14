using System;
using Infrastructure.Common;

namespace Infrastructure.Common.Assets
{
    public class AssetEntry : AuditableEntity, ICloneable
    {
        /// <summary>
        /// Asset language
        /// </summary>
        public string LanguageCode { get; set; }

        public TenantIdentity Tenant { get; set; }
        public BlobInfo BlobInfo { get; set; }

        /// <summary>
        /// User defined grouping (optional)
        /// </summary>
        public string Group { get; set; }

        #region ICloneable members

        public virtual object Clone()
        {
            var result = MemberwiseClone() as AssetEntry;

            result.Tenant = Tenant?.Clone() as TenantIdentity;
            result.BlobInfo = BlobInfo?.Clone() as BlobInfo;

            return result;
        }

        #endregion
    }
}
