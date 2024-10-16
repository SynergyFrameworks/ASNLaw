using System;

namespace Scion.Data.Common
{
    [AttributeUsageAttribute(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public class ParentAttribute : Attribute
	{
	}
}
