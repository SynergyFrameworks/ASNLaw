using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Modularity;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Scion.Infrastructure.Web.Swagger
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static string platformDocName { get; } = "Scion.Infrastructure";
        public static string platformUIDocName { get; } = "PlatformUI";
        private static string oauth2SchemeName = "oauth2";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            ManifestModuleInfo[] modules = provider.GetService<IModuleCatalog>().Modules.OfType<ManifestModuleInfo>().Where(m => m.ModuleInstance != null).ToArray();

            services.AddSwaggerGen(c =>
            {
                OpenApiInfo platformInfo = new OpenApiInfo
                {
                    Title = "scionanalytics Solution REST API documentation",
                    Version = "v1",
                    TermsOfService = new Uri("https://scionanalytics.com/terms"),
                    Description = "For this sample, you can use the key to satisfy the authorization filters.",
                    Contact = new OpenApiContact
                    {
                        Email = "support@scionanalytics.com",
                        Name = "Scion Analytics",
                        Url = new Uri("https://scionanalytics.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Scion Analytics Software License 1.0",
                        Url = new Uri("http://scionanalytics.com/opensourcelicense")
                    }
                };

                c.SwaggerDoc(platformDocName, platformInfo);
                c.SwaggerDoc(platformUIDocName, platformInfo);

                foreach (ManifestModuleInfo module in modules)
                {
                    c.SwaggerDoc(module.ModuleName, new OpenApiInfo { Title = $"{module.Id}", Version = "v1" });
                }

                c.TagActionsBy(api => api.GroupByModuleName(services));
                c.IgnoreObsoleteActions();
                c.DocumentFilter<ExcludeRedundantDepsFilter>();
                // This temporary filter removes broken "application/*+json" content-type.
                // It seems it's some openapi/swagger bug, because Autorest fails.
                c.OperationFilter<ConsumeFromBodyFilter>();
                c.OperationFilter<FileResponseTypeFilter>();
                c.OperationFilter<OptionalParametersFilter>();
                c.OperationFilter<ArrayInQueryParametersFilter>();
                c.OperationFilter<FileUploadOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.OperationFilter<TagsFilter>();
                c.SchemaFilter<EnumSchemaFilter>();
                c.SchemaFilter<SwaggerIgnoreFilter>();
                c.MapType<object>(() => new OpenApiSchema { Type = "object" });
                c.AddModulesXmlComments(services);
                c.CustomOperationIds(apiDesc =>
                    apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? $"{((ControllerActionDescriptor)apiDesc.ActionDescriptor).ControllerName}_{methodInfo.Name}" : null);
                c.AddSecurityDefinition(oauth2SchemeName, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Description = "OAuth2 Resource Owner Password Grant flow",
                    Flows = new OpenApiOAuthFlows()
                    {
                        Password = new OpenApiOAuthFlow()
                        {
                            TokenUrl = new Uri($"/connect/token", UriKind.Relative)
                        }
                    },
                });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (docName.EqualsInvariant(platformUIDocName)) return true; // It's an UI endpoint, return all to correctly build swagger UI page

                    Assembly currentAssembly = ((ControllerActionDescriptor)apiDesc.ActionDescriptor).ControllerTypeInfo.Assembly;
                    if (docName.EqualsInvariant(platformDocName) && currentAssembly.FullName.StartsWith(docName)) return true; // It's a platform endpoint. 
                    // It's a module endpoint. 
                    ManifestModuleInfo module = modules.FirstOrDefault(m => m.ModuleName.EqualsInvariant(docName));
                    return module != null && module.Assembly == currentAssembly;
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
            });

            // Unfortunately, we can't use .CustomSchemaIds, because it changes schema ids for all documents (impossible to change ids depending on document name).
            // But we need this, because PlatformUI document should contain ref schema ids as type.FullName to avoid conflict with same type names in different modules.
            // As a solution we use custom swagger generator that catches document name and generates schemaids depending on it
            services.AddTransient<ISwaggerProvider, CustomSwaggerGenerator>();

            //This is important line switches the SwaggerGenerator to use the Newtonsoft contract resolver that uses the globally registered PolymorphJsonContractResolver
            //to propagate up to the resulting OpenAPI schema the derived types instead of base domain types
            services.AddSwaggerGenNewtonsoftSupport();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void UseSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger(c =>
            {
                c.RouteTemplate = "docs/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    //TODO
                    //swagger.BasePath = $"{httpReq.Scheme}://{httpReq.Host.Value}";
                });

            });

            ManifestModuleInfo[] modules = applicationBuilder.ApplicationServices.GetService<IModuleCatalog>().Modules.OfType<ManifestModuleInfo>().Where(m => m.ModuleInstance != null).ToArray();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            applicationBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"./{platformUIDocName}/swagger.json", platformUIDocName);
                c.SwaggerEndpoint($"./{platformDocName}/swagger.json", platformDocName);
                foreach (ManifestModuleInfo module in modules)
                {
                    c.SwaggerEndpoint($"./{module.Id}/swagger.json", module.Id);
                }
                c.RoutePrefix = "docs";
                c.EnableValidator();
                c.IndexStream = () =>
                {
                    Stream type = typeof(Startup).GetTypeInfo().Assembly
                        .GetManifestResourceStream("Scion.Infrastructure.Web.wwwroot.swagger.index.html");
                    return type;
                };
                c.DocumentTitle = "scionanalytics Solution REST API documentation";
                c.InjectStylesheet("/swagger/vc.css");
                c.ShowExtensions();
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);
            });
        }


        /// <summary>
        /// grouping by Module Names in the ApiDescription
        /// with comparing Assemlies
        /// </summary>
        /// <param name="api"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IList<string> GroupByModuleName(this ApiDescription api, IServiceCollection services)
        {
            ServiceProvider providerSnapshot = services.BuildServiceProvider();
            ILocalModuleCatalog moduleCatalog = providerSnapshot.GetRequiredService<ILocalModuleCatalog>();

            // ------
            // Lifted from ApiDescriptionExtensions
            ControllerActionDescriptor actionDescriptor = api.GetProperty<ControllerActionDescriptor>();

            if (actionDescriptor == null)
            {
                actionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                api.SetProperty(actionDescriptor);
            }
            // ------

            Assembly moduleAssembly = actionDescriptor?.ControllerTypeInfo.Assembly ?? Assembly.GetExecutingAssembly();
            ModuleInfo groupName = moduleCatalog.Modules.FirstOrDefault(m => m.ModuleInstance != null && m.Assembly == moduleAssembly);

            return new List<string> { groupName != null ? groupName.ModuleName : "Platform" };
        }

        /// <summary>
        /// Add Comments/Descriptions from XML-files in the ApiDescription
        /// </summary>
        /// <param name="options"></param>
        /// <param name="services"></param>
        private static void AddModulesXmlComments(this SwaggerGenOptions options, IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            LocalStorageModuleCatalogOptions localStorageModuleCatalogOptions = provider.GetService<IOptions<LocalStorageModuleCatalogOptions>>().Value;

            string[] xmlCommentsDirectoryPaths = new[]
            {
                localStorageModuleCatalogOptions.ProbingPath,
                AppContext.BaseDirectory
            };

            foreach (string path in xmlCommentsDirectoryPaths)
            {
                string[] xmlComments = Directory.GetFiles(path, "*.XML");
                foreach (string xmlComment in xmlComments)
                {
                    options.IncludeXmlComments(xmlComment);
                }
            }
        }
    }
}
