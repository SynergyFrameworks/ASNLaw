using Infrastructure.Common.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Common
{
    public class Tenant : BasePersistantObject
    {
        public virtual string Name { get; set; }
        public virtual string BaseUrl { get; set; }
        public virtual string Logo { get; set; }
        public virtual string Settings { get; set; }
        public virtual string LogoPath { get; set; }
        public virtual string PdfLogoPath { get; set; }
        public virtual string LetterTemplatePath { get; set; }
        public virtual Dictionary<string, object> TenantSettings { get; set; }
        public virtual void CombineTenantSettings(JObject defaultTenantSettings, string defaultClientLogoPath, string defaultLetterPath)
        {
            JObject combinedSettings = new JObject(defaultTenantSettings);
            TenantSettings = JsonConvert.DeserializeObject<Dictionary<string, object>>(
                combinedSettings.ToString().Replace("\r\n", string.Empty),
                new JsonConverter[] { new CustomJsonConvertor(), });
            LogoPath = defaultClientLogoPath;
            if (TenantSettings.ContainsKey("NavigationBarLogo"))
            {
                LogoPath = defaultClientLogoPath + TenantSettings["NavigationBarLogo"];
            }
            if (TenantSettings.ContainsKey("PDFLogo"))
            {
                PdfLogoPath = TenantSettings["PDFLogo"].ToString();
            }

            JObject carrySettings = GetApplicationSettings("carry");
            if (carrySettings != null && carrySettings["LetterTemplatePath"] != null)
            {
                LetterTemplatePath = defaultLetterPath + carrySettings["LetterTemplatePath"];
            }
            Settings = JsonConvert.SerializeObject(TenantSettings);
        }


        public virtual JObject GetApplicationSettings(string appName)
        {
            JObject return_value = null;
            if (TenantSettings.ContainsKey("Applications"))
            {
                JArray applications = (JArray)TenantSettings["Applications"];
                if (applications.Count(a => a["Id"].ToString() == appName) > 0)
                {
                    return_value = JObject.FromObject(applications.First(a => a["Id"].ToString() == appName));
                }
            }

            return return_value;
        }

    }
}
