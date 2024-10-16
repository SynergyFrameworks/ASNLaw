using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Scion.Infrastructure.Modularity;
using Scion.Infrastructure.Settings;

namespace Scion.Infrastructure.Web.Swagger
{
    public class TagsFilter : IOperationFilter
    {
        private readonly IModuleCatalog _moduleCatalog;
        private readonly ISettingsManager _settingManager;

        public TagsFilter(IModuleCatalog moduleCatalog, ISettingsManager settingManager)
        {
            _moduleCatalog = moduleCatalog;
            _settingManager = settingManager;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerTypeInfo = ((ControllerActionDescriptor)context.ApiDescription.ActionDescriptor).ControllerTypeInfo;
            var module = _moduleCatalog.Modules
                .OfType<ManifestModuleInfo>()
                .Where(x => x.ModuleInstance != null)
                .FirstOrDefault(x => (controllerTypeInfo.Assembly == x.ModuleInstance.GetType().Assembly));

            if (module != null)
            {
                operation.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag() { Name = module.Title, Description = module.Description }
                };
            }
            else if (controllerTypeInfo.Assembly.GetName().Name == "Scion.Infrastructure.Web")
            {
                operation.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag() { Name = "ScionAnalytics platform" }
                };
            }
        }
    }
}
