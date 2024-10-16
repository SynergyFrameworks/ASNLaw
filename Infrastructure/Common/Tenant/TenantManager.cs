using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Domain.Settings;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Persistence;
using Infrastructure.Query.Settings;
using Infrastructure.Query.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Infrastructure.Common.TenantManagement
{
    public class TenantManager : ITenantManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TenantManager));
        public IEntityManager EntityManager { get; set; }
        public string DefaultTenantUrl { get; set; }
        public string DevelopmentServerHost { get; set; }
        public string DevelopmentApi { get; set; }
        public string DefaultLetterPath { get; set; }
        public string DefaultClientLogoPath { get; set; }
        public IReadOnlyDataManager ReadOnlyDataManager { get; set; }

        //the tenant specific settings imposed on the default settings.
        public Dictionary<Guid, Dictionary<string, object>> CurrentTenantSettings = new Dictionary<Guid, Dictionary<string, object>>();
        public string DefaultLogoString { get; set; }
        public string LocalTenant { get; set; }

        public string GetTenantLogo(string tenantUrl)
        {
            if (tenantUrl.Equals(DevelopmentServerHost))
            {
                Tenant currentTenant = GetCurrentTenantByUrl(tenantUrl);
                string baseUrl = new Uri(currentTenant.BaseUrl).Host;
                TenantLogoByUrl currentTenantLogo = ReadOnlyDataManager.Find<TenantLogoByUrl>(new Criteria { Parameters = new SortedDictionary<string, object> { { "baseUrl", baseUrl } } });
                return currentTenantLogo.Logo ?? DefaultLogoString;
            }
            Criteria criteria = new Criteria
            {
                Parameters = new SortedDictionary<string, object>
                {
                    {"baseUrl", tenantUrl}
                }
            };
            TenantLogoByUrl tenant = ReadOnlyDataManager.Find<TenantLogoByUrl>(criteria);
            if (tenant == null)
                return DefaultLogoString;
            return tenant.Logo ?? DefaultLogoString;
        }

        public Tenant GetCurrentTenantByUrl(string tenantUrl)
        {
            try
            {
                Log.DebugFormat("Retreiving Tenant for url: {0}", tenantUrl);
                if (tenantUrl.Contains("localhost"))
                {
                    TenantByName localTenant = ReadOnlyDataManager.Find<TenantByName>(new Criteria { Parameters = new SortedDictionary<string, object> { { "Name", LocalTenant } } });

                    localTenant.CombineTenantSettings(GetTenantSettings(localTenant.Id), DefaultClientLogoPath, DefaultLetterPath);
                    return localTenant;
                }

                //we just want the host name
                string url = tenantUrl;

                if (url.StartsWith("http"))
                    url = new Uri(tenantUrl).Host;

                TenantByUrl tenant = ReadOnlyDataManager.Find<TenantByUrl>(new Criteria { Parameters = new SortedDictionary<string, object> { { "baseUrl", url } } });

                tenant.CombineTenantSettings(GetTenantSettings(tenant.Id), DefaultClientLogoPath, DefaultLetterPath);
                Log.DebugFormat("Found tenant {0}", tenant.Name);
                return tenant;
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Equals("Sequence contains no matching element"))
                    return null;
                throw;
            }
        }

        public IEnumerable<Tenant> GetTenants()
        {
            return ReadOnlyDataManager.FindAll<Tenant>(new Criteria());
        }




        private Guid GetTenantIdByThread()
        {
            //TODO: What if the tenatId is Guid.Empty?
            return Thread.CurrentThread.GetAssignedTenantId();
        }

        public Tenant GetCurrentTenantByThread()
        {
            Guid tenantId = GetTenantIdByThread();
            if (tenantId == Guid.Empty)
                return null;
           Tenant tenant = ReadOnlyDataManager.Find<Tenant>(new Criteria { Parameters = new SortedDictionary<string, object> { { "specifiedTenantId", tenantId } } });
            tenant.CombineTenantSettings(GetTenantSettings(tenantId), DefaultClientLogoPath, DefaultLetterPath);
            return tenant;
        }

        public Tenant GetCurrentFullTenantByThread()
        {
            Guid tenantId = GetTenantIdByThread();
            if (tenantId == Guid.Empty)
                return null;
            return ReadOnlyDataManager.Find<Tenant>(new Criteria { Parameters = new SortedDictionary<string, object> { { "tenantId", tenantId } } });
        }


        //called on load after login, as well as after hard refresh.
        //if we cannot find the tenant or the default settings, return error response.
        //if we can find the tenant but they have no settings, return error response.
        //if there is an error parsing json, return error response.
        public Dictionary<string, object> GetTenantSettings(string host = null)
        {

            Guid tenantId = GetTenantIdByThread();

            if (!CurrentTenantSettings.ContainsKey(tenantId))
            {
                InitializeSettingsObjects(tenantId);
            }


            if (CurrentTenantSettings[tenantId] == null || !CurrentTenantSettings[tenantId].Any())
            {
                Tenant currentTenant = GetCurrentTenantByThread();
                if (!string.IsNullOrEmpty(currentTenant.Settings))
                {
                    Guid userId = Thread.CurrentThread.GetUserId();

                    Criteria defaultCriteria = new Criteria { Parameters = new SortedDictionary<string, object> { { "findMode", "applicationSettingsQuery" }, { "userId", userId }, { "tenant_Id", tenantId.ToString() } } };
                    IList<TenantSetting> topSettings = ReadOnlyDataManager.FindAll<TenantSetting>(defaultCriteria);
                    Dictionary<string, object> tenantSettings = InitializeTenantSettings(topSettings, tenantId);
                    currentTenant.TenantSettings = tenantSettings;

                    if (currentTenant.TenantSettings != null)
                    {
                        CurrentTenantSettings[tenantId] = currentTenant.TenantSettings;
                        if (CurrentTenantSettings[tenantId] != null)
                        {
                            return CurrentTenantSettings[tenantId];
                        }
                    }
                }
            }
            //settings were already loaded
            return CurrentTenantSettings[tenantId];
        }


        public JObject GetTenantSettings(Guid tenantId)
        {
            Criteria defaultCriteria = new Criteria { Parameters = new SortedDictionary<string, object> { { "findMode", "applicationSettingsQuery" }, { "userId", null }, { "tenant_Id", tenantId.ToString() } } };

            IList<TenantSetting> topSettings = ReadOnlyDataManager.FindAll<TenantSetting>(defaultCriteria);

            Dictionary<string, object> tenantSettings = InitializeTenantSettings(topSettings, tenantId);

            string tenantSettingsJsonString = JsonConvert.SerializeObject(tenantSettings);

            return JObject.Parse(tenantSettingsJsonString);
        }
        /// <summary>
        /// Sets the applications and application settings for the tenant
        /// </summary>
        /// <param name="topSettings"></param>
        /// <param name="tenantId">If a tenant Id is not included, the thread's tenantId will be used</param>
        /// <returns></returns>
        public Dictionary<string, object> InitializeTenantSettings(IList<TenantSetting> topSettings, Guid? tenantId = null)
        {
            Guid thread_tenant_id = GetTenantIdByThread();
            tenantId = tenantId.HasValue ? tenantId : thread_tenant_id;
            //When on the login screen, the thread Id is not set, but is sent in for the initializing.
            //Getting the tenant applications uses the thread Id, but this isn't needed on the login screen.
            IList<TenantApplication> tenant_applications = GetTenantApplications();
            Dictionary<string, object> tenantSetting = new Dictionary<string, object>
            {
                ["Applications"] = new List<Dictionary<string, object>>(),
                ["TenantId"] = tenantId
            };
            foreach (TenantSetting setting in topSettings.Where(t => String.IsNullOrEmpty(t.Application.Name)))
            {
                string[] keyArray = setting.Key.Split('.');
                string key = keyArray[keyArray.Length - 1];
                tenantSetting[key] = GetSettingValue(setting.Value);
            }
            foreach (TenantApplication application in tenant_applications)
            {
                Dictionary<string, object> temp_dictionary = new Dictionary<string, object>();
                JObject application_settings = JObject.Parse(application.Settings);
                JToken appName = application_settings.ContainsKey("moduleName") ? application_settings["moduleName"] : application.Name;

                temp_dictionary.Add("Name", application.Name);
                temp_dictionary.Add("ApplicationId", application.ApplicationId);
                temp_dictionary.Add("Id", appName);
                temp_dictionary.Add("RoutePrefix", "/" + appName);
                temp_dictionary.Add("IsEnabled", application.IsEnabled != 0);
                if (thread_tenant_id == tenantId)
                {
                    temp_dictionary.Add("ImportTypes", JToken.FromObject(GetImportTypes(application.ApplicationId)));
                    temp_dictionary.Add("MenuEntries", JToken.FromObject(GetMenuItems(application.ApplicationId)));
                    temp_dictionary.Add("Settings", JToken.FromObject(InitializeApplicationSettings(application.ApplicationId)));
                    temp_dictionary.Add("Views", JToken.FromObject(GetViewSettings(application.ApplicationId)));
                }



                foreach (TenantSetting setting in topSettings.Where(t => t.Application.Name == application.Name))
                {
                    string[] keyArray = setting.Key.Split('.');
                    string key = keyArray[keyArray.Length - 1];
                    if (temp_dictionary.ContainsKey(key))
                    {
                        Log.Debug($"Key {key} already exists.  Current value: {temp_dictionary[key].ToString()} New Value: { GetSettingValue(setting.Value).ToString()}");
                    }
                    temp_dictionary[key] = GetSettingValue(setting.Value);
                }

                ((List<Dictionary<string, object>>)tenantSetting["Applications"]).Add(temp_dictionary);
            }

            return tenantSetting;
        }

        /**
         * Gets the application settings for the tenant
         */
        public Dictionary<string, object> InitializeApplicationSettings(Guid appid)
        {
            // Get the Tenant's applications, and give them entries in the settings
            string userId = null;
            // Removed for now
            // Causing errors, since we are trying to get User Id before User is Authenticated
            // TODO: Update to get User Id and include User Id to get tenant Settings
            /*
            string userId = string.Empty;
            try
             {
                 userId = Thread.CurrentThread.GetUserId().ToString();
             }
             catch (NullReferenceException e) {
                 userId = Guid.Empty.ToString();
             }*/

            string tenantId = GetTenantIdByThread().ToString();
            string applicationId = appid.ToString();
            Criteria criteria = new Criteria { Parameters = new SortedDictionary<string, object> { { "findMode", "applicationSettingsQuery" }, { "applicationId", applicationId }, { "userId", userId }, { "tenant_Id", tenantId.ToString() } } };
            IList<TenantSetting> defaultSettings = ReadOnlyDataManager.FindAll<TenantSetting>(criteria);

            Dictionary<string, object> returnSettings = new Dictionary<string, object>();
            foreach (TenantSetting setting in defaultSettings)
            {
                string[] keyArray = setting.Key.Split('.');
                string key = keyArray[keyArray.Length - 1];
                returnSettings[key] = GetSettingValue(setting.Value);
            }

            return returnSettings;
        }

        private object GetSettingValue(string setting)
        {
            if (int.TryParse(setting, out int n))
            {
                return n;
            }
            else
            {
                if (setting == "true")
                {
                    return true;
                }
                else if (setting == "false")
                {
                    return false;
                }
                else
                {
                    return setting;
                }
            }
        }

        public IList<MenuItem> GetMenuItems(Guid applicationId)
        {
            Criteria criteria = new Criteria { Parameters = new SortedDictionary<string, object> { { "applicationId", applicationId } } };
            criteria.SortOrders.Add("DisplayOrder", SortOrder.ASC);
            IList<MenuItem> menuItems = ReadOnlyDataManager.FindAll<MenuItem>(criteria);

            return menuItems;
        }

        public IList<ImportType> GetImportTypes(Guid applicationId)
        {
            Criteria criteria = new Criteria { Parameters = new SortedDictionary<string, object> { { "applicationId", applicationId } } };
            IList<ImportType> importTypes = ReadOnlyDataManager.FindAll<ImportType>(criteria);

            return importTypes;
        }

        public Dictionary<string, object> GetViewSettings(Guid ApplicationId)
        {
            Dictionary<string, object> viewSettings = new Dictionary<string, object>();
            foreach (ApplicationView view in GetViews(ApplicationId))
            {
                viewSettings.Add(view.Name, new Dictionary<string, object>());
                foreach (ApplicationViewItem viewItem in GetViewItems(view.Id))
                {
                    ((Dictionary<string, object>)viewSettings[view.Name]).Add(viewItem.Name, GetSettingValue(viewItem.Value));
                }
            }
            return viewSettings;
        }

        public IList<ApplicationView> GetViews(Guid applicationId)
        {
            var criteria = new Criteria { Parameters = new SortedDictionary<string, object> { { "applicationId", applicationId } } };
            IList<ApplicationView> views = ReadOnlyDataManager.FindAll<ApplicationView>(criteria);

            return views;
        }
        public IList<ApplicationViewItem> GetViewItems(Guid viewId)
        {
            var criteria = new Criteria { Parameters = new SortedDictionary<string, object> { { "viewId", viewId } } };
            IList<ApplicationViewItem> viewItems = ReadOnlyDataManager.FindAll<ApplicationViewItem>(criteria);

            return viewItems;
        }

        public IList<TenantApplication> GetTenantApplications()
        {
            //Uses the thread tenant ID, which is set after login. Before login, the applications aren't needed, which shouldn't be a problem.
            Criteria criteria = new Criteria { Parameters = new SortedDictionary<string, object> { } };
            return ReadOnlyDataManager.FindAll<TenantApplication>(criteria);
        }

        public Tenant GetTenantByName(string tenantName)
        {
            return ReadOnlyDataManager.Find<TenantByName>(new Criteria { Parameters = new SortedDictionary<string, object> { { "Name", tenantName } } });
        }

        private void InitializeSettingsObjects(Guid tenantId)
        {
            CurrentTenantSettings.Add(tenantId, new Dictionary<string, object>());
        }

        public void InitializeSettings()
        {
        }



        public void SaveSettings(JObject settingsJsonObject)
        {
            try
            {
                string settingsJson = settingsJsonObject.ToString();
                Guid tenantId = GetTenantIdByThread();
                if (tenantId == Guid.Empty)
                    return;
                Tenant currentTenant = EntityManager.Find<Tenant>(tenantId);
                currentTenant.Settings = settingsJson;
                EntityManager.CreateOrUpdate(currentTenant);
                EntityManager.Flush();
                //remove cached settings to force reload on next get, otherwise users will get cached settings rather than persisted.
                CurrentTenantSettings.Remove(tenantId);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Fatal error saving tenant settings: {0}", e);
                throw;
            }
        }


        public void UpdateMenuItems(IList<MenuItem> menuItems)
        {
            try
            {
                foreach (Domain.Settings.MenuItem item in menuItems)
                {
                    Log.DebugFormat($"Setting edited to Menu Item Name: {item.Name} , Value: {item.IsEnabled}");
                    MenuItem existingSetting = EntityManager.Find<Domain.Settings.MenuItem>(item.Id);
                    if (existingSetting != null)
                    {
                        existingSetting.ShowInSidebar = item.ShowInSidebar;
                        EntityManager.CreateOrUpdate(existingSetting);
                        // var activity = ActivityManager.CreateActivityObject(item.Id, $"Setting edited to Menu Item, Name: {existingSetting.Name} , Value: {existingSetting.IsEnabled}");
                        // ActivityManager.SaveActivity(activity);
                    }
                }
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Fatal error saving menu item settings: {0}", e);
                throw;
            }
        }



        public void UpdateLogoImage(string logoImage)
        {
            try
            {
                Guid tenantId = Thread.CurrentThread.GetAssignedTenantId();
                string key = "Customization.General.NavigationBarLogo";
                TenantSetting existingLogo = EntityManager.FindByNamedQuery<Domain.Settings.TenantSetting>("FindByKey", new Dictionary<string, object> { { "tenantId", tenantId }, { "key", key } });

                if (existingLogo != null)
                {
                    existingLogo.Value = logoImage;
                    EntityManager.CreateOrUpdate(existingLogo);
                }
                else
                {
                    TenantSetting logoSetting = new Domain.Settings.TenantSetting
                    {
                        Key = "Customization.General.NavigationBarLogo",
                        Value = logoImage,
                        Name = "Navigation Bar Logo",
                        Description = "",
                        DataType = "picture"
                    };

                    EntityManager.CreateOrUpdate(logoSetting);

                }

                CurrentTenantSettings.Remove(tenantId);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Fatal error saving menu item settings: {0}", e);
                throw;
            }
        }


        public void ClearSettings()
        {
            try
            {
                Guid tenantId = GetTenantIdByThread();
                //remove cached settings to force reload on next get, otherwise users will get cached settings rather than persisted.
                CurrentTenantSettings.Remove(tenantId);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Fatal error clearing tenant settings: {0}", e);
                throw;
            }
        }

        public JObject GetBaseTenant()
        {
            Criteria defaultCriteria = new Criteria { Parameters = new SortedDictionary<string, object> { { "findMode", "topmost" }, { "userId", null } } };

            IList<TenantSetting> topSettings = ReadOnlyDataManager.FindAll<TenantSetting>(defaultCriteria);

            Dictionary<string, object> tenantSettings = InitializeTenantSettings(topSettings);
            string tenantSettingsJsonString = JsonConvert.SerializeObject(tenantSettings);
            return JObject.Parse(tenantSettingsJsonString);
        }

        public Tenant GetCurrentTenantById(Guid tenantId)
        {
            var tenant = ReadOnlyDataManager.Find<Tenant>(new Criteria { Parameters = new SortedDictionary<string, object> { { "specifiedTenantId", tenantId } } });

            tenant.CombineTenantSettings(GetTenantSettings(tenantId), DefaultClientLogoPath, DefaultLetterPath);
            return tenant;
        }

        public IList<TenantKeywordMapping> GetKeywordMappings(Guid tenantId)
        {
            Criteria criteria = new Criteria
            {
                Parameters = new SortedDictionary<string, object> { { "tenant_Id", tenantId } }
            };

            IList<TenantKeywordMapping> results = ReadOnlyDataManager.FindAll<TenantKeywordMapping>(criteria);
            if (results == null)
            {
                results = new List<TenantKeywordMapping>();
            }
            return results;
        }
    }
}
