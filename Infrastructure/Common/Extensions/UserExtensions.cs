using System;
using System.Linq;
using Infrastructure.Common;
using Infrastructure.Common.Domain.Users;

namespace Infrastructure.Common.Extensions
{
    public static class UserExtensions
    {
        public static TenantIdentity GetDefaultTenant(this User user, string requestUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(requestUrl))
                {
                    //    find a tenant that matches this host
                    var requestHost = new Uri(requestUrl).Host;

                    //    var selectedTenant =
                    //        user.Tenant.FirstOrDefault(
                    //            tenant => string.Equals(requestHost, new Uri(Tenant.BaseUrl).Host, StringComparison.Ordinal));

                    //    if (selectedTenant != null)
                    //    {
                    //        return selectedTenant;
                    //    }
                }


                //if (user.Tenants.Any(t => t.Name.Equals("Riverstone Holdings")))
                //    return user.Tenants.First(t => t.Name.Equals("Riverstone Holdings"));

                /*
                if (user.Tenants.Any(t => t.Name.Equals("Scion Analytics")))
                    return user.Tenants.First(t => t.Name.Equals("Scion Analytics"));

                if (user.Tenants.Any(t => t.Name.Equals("WHAT EVER")))
                    return user.Tenants.First(t => t.Name.Equals("WHAT EVER"));
                */
                throw new Exception("Unable to assign tenant configuration");
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding tenant: " + ex.Message);
            }
        }
    }
}