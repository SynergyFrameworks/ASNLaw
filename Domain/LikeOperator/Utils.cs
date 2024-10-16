namespace Domain.LikeOperator
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Globalization;
	using System.Reflection;
	using System.Resources;
	using System.Runtime.InteropServices;
	using System.Security;
	using System.Security.Permissions;
	using System.Text;
	using System.Threading;

  public sealed class Utils
  {
    internal static Encoding GetFileIOEncoding() => Encoding.Default;
    internal static CultureInfo GetCultureInfo() => Thread.CurrentThread.CurrentCulture;
  }
}

