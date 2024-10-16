using Infrastructure.Common;

namespace Infrastructure.DynamicProperties
{
    public class DynamicPropertySearchCriteria : SearchCriteriaBase
    {
        public string TypeName => ObjectType;
    }
}
