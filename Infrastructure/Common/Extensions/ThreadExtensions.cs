using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using log4net;
using Newtonsoft.Json.Linq;
using Infrastructure.Common;
using Infrastructure.Common.Extensions;

namespace Infrastructure.Common.Extensions
{
    public static class ThreadExtensions
    {
        [ThreadStatic]
        public static bool IsParticipantPortalRequest;
        private static readonly ILog log = LogManager.GetLogger(typeof(ThreadExtensions));

        public static Guid GetAssignedTenantId(this Thread thread)
        {
            var tenant = Thread.CurrentThread.GetAssignedTenant();
            var tenantId = tenant?.GetPropertyValue("Id");
            if (tenantId is Guid guid)
                return guid;

            if (Thread.CurrentPrincipal == null)
                return Guid.Empty;

            var identity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var tenantClaim = identity.FindFirst(TenantClaim.TenantId);

            return tenantClaim == null ? Guid.Empty : Guid.Parse(tenantClaim.Value);
        }

        public static Dictionary<string,object> GetAssignedTenantSettings(this Thread thread)
        {
            var tenant = Thread.CurrentThread.GetAssignedTenant();
            return (Dictionary<string,object>) tenant?.GetPropertyValue("TenantSettings");
        }

        /// <summary>
        /// Get a specific setting for the current tenant.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="setting">
        /// Name of the setting.
        /// If it is nested, use a dot delimited string to index to it.
        /// If a settings level is an array, signify an identifier to match an object at that level (first will be taken)
        ///     e.g. "Applications.[Id:carry].Name" should path to the name of the application with Id carry.
        /// </param>
        /// <returns>The setting if it exists, otherwise default(T).</returns>
        public static T GetAssignedTenantSetting<T>(this Thread thread, string setting)
        {
            try
            {
                if (string.IsNullOrEmpty(setting))
                    return default(T);

                var parsedSetting = setting.Split('.');
                var settings = thread.GetAssignedTenantSettings();

                if (settings == null)
                    return default(T);

                object result = settings;

                foreach (var settingLevel in parsedSetting)
                {
                    try
                    {
                        if (settingLevel.StartsWith("["))
                        {
                            var split = settingLevel.Split(':');
                            var key = split[0].Substring(1);
                            var value = split[1].Substring(0, split[1].Length - 1);

                            var array = result as JArray;

                            //result = array.FirstOrDefault(token => token[key].SafeToString() == value);
                            foreach (var token in array)
                            {
                                if (token[key].ToString() != value)
                                    continue;

                                result = token;
                                break;
                            }
                        }
                        else
                        {
                            result = result is Dictionary<string, object> objects ? objects[settingLevel] : ((JObject)result)[settingLevel];
                        }
                    }
                    catch (Exception ex)
                    {
                        log.WarnFormat("Error finding tenant setting {0} at level {1} of {2}: {3}", parsedSetting.Last(), settingLevel, setting, ex);
                        return default(T);
                    }
                }
                if (result == null)
                    return default(T);

                return result is JToken jToken ? jToken.ToObject<T>() : (T)result;
            }
            catch (Exception ex)
            {
                var tenantId = thread.GetAssignedTenantId();
                log.WarnFormat("Error attempting to find setting {0} as type {3} for tenant {1}: {2}", setting, tenantId, ex, typeof(T).Name);
                return default(T);
            }
        }

        public static JObject GetAssignedApplicationTenantSettings(this Thread thread,string appName)
        {
            var settings = Thread.CurrentThread.GetAssignedTenantSettings();
            var apps = (JArray)settings["Applications"];
            var app = apps.Children<JObject>()
                .FirstOrDefault(d => d["Id"].Value<string>() == appName);

            return app?["Settings"]?.Value<JObject>();
        }

        public static object GetAssignedTenant(this Thread thread)
        {
            try
            {
                return Thread.GetData(Thread.GetNamedDataSlot("tenant"));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetReturnUrl(this Thread thread)
        {
            try
            {
                return (string)Thread.GetData(Thread.GetNamedDataSlot("returnUrl"));
            }
            catch
            {
                return null;
            }
        }

        public static bool IsLpTenant(this Thread thread)
        {
            try
            {
                var settings = Thread.CurrentThread.GetAssignedTenantSettings();

                if (!settings.ContainsKey("IsLPTenant") || settings["IsLPTenant"] == null || string.IsNullOrEmpty(settings["IsLPTenant"].ToString()))
                {
                    return false;
                }

                return (bool)settings["IsLPTenant"];
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Guid GetUserId(this Thread thread)
        {
            var identity = (ClaimsIdentity) Thread.CurrentPrincipal.Identity;

            return Guid.Parse(identity.FindFirst(ClaimTypes.Role).Value);
        }

        public static void SetIsParticipantPortalFlag(this Thread thread, bool isParticipantPortal)
        {
            IsParticipantPortalRequest = isParticipantPortal;
        }

        public static bool GetIsParticipantPortalFlag(this Thread thread)
        {
            return IsParticipantPortalRequest;
        }

        public static void InitializeThread(this Thread thread)
        {
            Thread.CurrentThread.SetIsParticipantPortalFlag(false);
        }
    }
}
