using System;

namespace Infrastructure.Swagger
{
    /// <summary>
    /// Mark with this attribute all API methods which returned stream response
    /// for  correct generation  swagger API document schema
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class SwaggerFileResponseAttribute : Attribute
    {
    }
}
