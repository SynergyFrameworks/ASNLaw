using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Infrastructure.Common.Domain.Settings;

namespace Infrastructure.Common.TenantManagement
{
    public interface ITenantManager
    {
        string GetTenantLogo(string tenantUrl);
        Dictionary<string, object> GetTenantSettings(string host = null);
        IList<Query.Tenant.TenantKeywordMapping> GetKeywordMappings(Guid tenantId);
        Tenant GetTenantByName(string tenantName);
        Tenant GetCurrentTenantById(Guid tenantId);
        Tenant GetCurrentTenantByThread();
        Tenant GetCurrentTenantByUrl(string tenantUrl);
        IEnumerable<Tenant> GetTenants();
        Tenant GetCurrentFullTenantByThread();
        JObject GetBaseTenant();
        void SaveSettings(JObject settingsJsonObject);
        void ClearSettings();
        void UpdateMenuItems(IList<MenuItem> menuitems);
        void UpdateLogoImage(string logoImage);
    }
}
