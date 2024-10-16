using Microsoft.OpenApi.Models;
using Scion.Infrastructure.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Scion.Infrastructure.Web.Swagger
{
    /// <summary>
    /// Allows to ignore <see cref="Newtonsoft.Json.JsonIgnoreAttribute"/> and <see cref="SwaggerIgnoreAttribute"/>.
    /// </summary>
    public class SwaggerIgnoreFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            System.Type type = context.Type;
            foreach (System.Reflection.PropertyInfo prop in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                     .Where(p => p.GetCustomAttributes(typeof(SwaggerIgnoreAttribute), true)?.Any() == true ||
                                     p.GetCustomAttributes(typeof(Newtonsoft.Json.JsonIgnoreAttribute), true)?.Any() == true))
            {
                string propName = prop.Name[0].ToString().ToLower() + prop.Name.Substring(1);
                if (schema?.Properties?.ContainsKey(propName) == true)
                    schema?.Properties?.Remove(propName);
            }
        }
    }
}
