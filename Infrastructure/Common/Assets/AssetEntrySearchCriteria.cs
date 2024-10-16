using Infrastructure.Common;
using Infrastructure.Swagger;

namespace Infrastructure.Common.Assets
{
    [SwaggerSchemaId("AssetEntrySearchCriteria")]
    public class AssetEntrySearchCriteria : SearchCriteriaBase
    {
        public TenantIdentity[] Tenants { get; set; }


        public string Group { get; set; }
    }
}
