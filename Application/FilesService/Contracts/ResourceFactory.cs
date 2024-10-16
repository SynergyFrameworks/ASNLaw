using Microsoft.Extensions.Configuration;
using Scion.FilesService.Contracts.Sql;
using Scion.Infrastructure.Tenant;
using System;
using System.Configuration;

namespace Scion.FilesService.Contracts
{
    public class ResourceFactory : IResourceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITenantManager _tenantManager;
        private readonly IConfiguration _configuration;
        public ResourceFactory(IServiceProvider serviceProvider, ITenantManager tenantManager, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _tenantManager = tenantManager;
            _configuration = configuration;
        }

        public IFileManager GetFileManager()
        {
            //var tenantSettings = _tenantManager.GetTenantSettings();
            //TODO: Uncomment when new manager is implemented...
            //if ((Enums.Resource) Enum.Parse(typeof(Enums.Resource),  tenantSettings["DataType"].ToString()) == Enums.Resource.SQL)
            var manager = (IFileManager)_serviceProvider.GetService(typeof(Sql.SqlManager));
            manager.Source = new SqlProvider();
            _configuration.Bind("SqlProvider", manager.Source);

            return (IFileManager)_serviceProvider.GetService(typeof(Sql.SqlManager));
        }
    }
}
