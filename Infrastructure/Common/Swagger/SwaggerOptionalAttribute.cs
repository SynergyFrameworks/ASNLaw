using System;

namespace Infrastructure.Swagger
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public class SwaggerOptionalAttribute : Attribute
    {
    }
}
