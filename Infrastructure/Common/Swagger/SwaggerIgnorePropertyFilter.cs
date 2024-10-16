using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Text.Json.Serialization;

namespace Infrastructure.Swagger
{
    public class SwaggerIgnorePropertyFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription == null || operation.Parameters == null)
                return;

            if (!context.ApiDescription.ParameterDescriptions.Any())
                return;

            // Handle form-bound parameters with [JsonIgnore]
            var formParams = context.ApiDescription.ParameterDescriptions.Where(p => p.Source.Equals(BindingSource.Form)
                        && p.CustomAttributes().Any(attr => attr.GetType().Equals(typeof(JsonIgnoreAttribute))));

            foreach (var p in formParams)
            {
                var requestBodyContent = operation.RequestBody.Content.Values.SingleOrDefault();
                if (requestBodyContent != null)
                {
                    requestBodyContent.Schema.Properties.Remove(p.Name);
                }
            }

            // Handle query-bound parameters with [JsonIgnore]
            var queryParams = context.ApiDescription.ParameterDescriptions.Where(p => p.Source.Equals(BindingSource.Query)
                        && p.CustomAttributes().Any(attr => attr.GetType().Equals(typeof(JsonIgnoreAttribute))));

            foreach (var p in queryParams)
            {
                var parameterToRemove = operation.Parameters.SingleOrDefault(w => w.Name.Equals(p.Name));
                if (parameterToRemove != null)
                {
                    operation.Parameters.Remove(parameterToRemove);
                }
            }
        }
    }
}
