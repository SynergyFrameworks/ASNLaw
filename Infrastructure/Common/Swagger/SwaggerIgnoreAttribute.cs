using System;

namespace Infrastructure.Swagger
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class SwaggerIgnoreAttribute : Attribute
    {
    }
}
