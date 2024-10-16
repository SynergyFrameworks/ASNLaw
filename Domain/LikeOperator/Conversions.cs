namespace Domain.LikeOperator
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Dynamic;
	using System.Globalization;
	using System.Reflection;
	using System.Security;
	using System.Security.Permissions;
	using System.Threading;

	public sealed class Conversions
	{
		private Conversions()
		{
		}

    public static string ToString(char Value) => Value.ToString();
	}
}
