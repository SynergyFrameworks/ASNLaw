
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Domain.LikeOperator
{
 
	class UnsafeNativeMethods
	{
    [SecurityCritical]
    [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
    internal static extern int LCMapStringA(
      int Locale,
      int dwMapFlags,
      [MarshalAs(UnmanagedType.LPArray)] byte[] lpSrcStr,
      int cchSrc,
      [MarshalAs(UnmanagedType.LPArray)] byte[] lpDestStr,
      int cchDest);

    [SecurityCritical]
    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern int LCMapString(
      int Locale,
      int dwMapFlags,
      [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpSrcStr,
      int cchSrc,
      [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpDestStr,
      int cchDest);
	}
}
