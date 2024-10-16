namespace Domain.LikeOperator
{
	using System;
	using System.Globalization;
	using System.Security;
	using System.Security.Permissions;
	using System.Text;
	using System.Threading;

   public static class StringExtensions
   {
      public static bool Like(this string str, string pattern)
      {
         return Domain.LikeOperator.LikeOperator.LikeString( str, pattern, CompareMethod.Binary );
		}
	}

  internal sealed class Strings
  {
    /// <summary>Returns an integer value representing the character code corresponding to a character.</summary>
    /// <param name="String">Required. Any valid <see langword="Char" /> or <see langword="String" /> expression. If <paramref name="String" /> is a <see langword="String" /> expression, only the first character of the string is used for input. If <paramref name="String" /> is <see langword="Nothing" /> or contains no characters, an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>The character code corresponding to a character.</returns>
    public static int Asc(char String)
    {
      int int32 = Convert.ToInt32(String);
      if (int32 < 128)
        return int32;
      try
      {
        Encoding fileIoEncoding = Utils.GetFileIOEncoding();
        char[] chars = new char[1]{ String };
        if (fileIoEncoding.IsSingleByte)
        {
          byte[] bytes = new byte[1];
          fileIoEncoding.GetBytes(chars, 0, 1, bytes, 0);
          return (int) bytes[0];
        }
        byte[] bytes1 = new byte[2];
        if (fileIoEncoding.GetBytes(chars, 0, 1, bytes1, 0) == 1)
          return (int) bytes1[0];
        if (BitConverter.IsLittleEndian)
        {
          byte num = bytes1[0];
          bytes1[0] = bytes1[1];
          bytes1[1] = num;
        }
        return (int) BitConverter.ToInt16(bytes1, 0);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns an integer value representing the character code corresponding to a character.</summary>
    /// <param name="String">Required. Any valid <see langword="Char" /> or <see langword="String" /> expression. If <paramref name="String" /> is a <see langword="String" /> expression, only the first character of the string is used for input. If <paramref name="String" /> is <see langword="Nothing" /> or contains no characters, an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>The character code corresponding to a character.</returns>
    public static int Asc(string String)
    {
      switch (String)
      {
        case "":
        case null:
          throw new ArgumentException("String is empty!");
        default:
          return Strings.Asc(String[0]);
      }
    }

    internal static bool IsValidCodePage(int codepage)
    {
      bool flag = false;
      try
      {
        if (Encoding.GetEncoding(codepage) != null)
          flag = true;
      }
      catch (StackOverflowException ex)
      {
        throw ex;
      }
      catch (OutOfMemoryException ex)
      {
        throw ex;
      }
      catch (ThreadAbortException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
      }
      return flag;
    }

    [SecuritySafeCritical]
    internal static string vbLCMapString(CultureInfo loc, int dwMapFlags, string sSrc)
    {
      int num1 = sSrc != null ? sSrc.Length : 0;
      string str;
      if (num1 == 0)
      {
        str = "";
      }
      else
      {
        int lcid = loc.LCID;
        Encoding encoding = Encoding.GetEncoding(loc.TextInfo.ANSICodePage);
        int num2;
        if (!encoding.IsSingleByte)
        {
          string s = sSrc;
          byte[] bytes = encoding.GetBytes(s);
          int cchDest = UnsafeNativeMethods.LCMapStringA(lcid, dwMapFlags, bytes, bytes.Length, (byte[]) null, 0);
          byte[] numArray = new byte[checked (cchDest - 1 + 1)];
          num2 = UnsafeNativeMethods.LCMapStringA(lcid, dwMapFlags, bytes, bytes.Length, numArray, cchDest);
          str = encoding.GetString(numArray);
        }
        else
        {
          string lpDestStr = new string(' ', num1);
          num2 = UnsafeNativeMethods.LCMapString(lcid, dwMapFlags, ref sSrc, num1, ref lpDestStr, num1);
          str = lpDestStr;
        }
      }
      return str;
    }

  }
}
