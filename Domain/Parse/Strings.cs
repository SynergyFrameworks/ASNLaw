namespace Domain.Parse
{
	using System;

	public static class Strings
	{
		public static int Len( string str ) => str.Length;

		public static string Mid(string str, int Start, int Length)
		{
			if (Start <= 0)
			{
				throw new ArgumentException($"{nameof(Start)} is less than or equal to zero!");
			}
			if (Length < 0)
			{
				throw new ArgumentException($"{nameof(Length)} is less than zero!");
			}
			if (Length == 0 || str == null)
			{
				return string.Empty;
			}
			int length = str.Length;
			if (Start > length)
			{
				return "";
			}
			if (Start + Length > length)
			{
				return str.Substring(Start - 1);
			}
			return str.Substring(Start - 1, Length);
		}

		public static string Mid(string str, int Start)
		{
			if (str == null)
			{
				return null;
			}
			return Mid(str, Start, str.Length);
		}

		public static bool IsNumeric(string str)
		{
			return int.TryParse( str, out int _ );
		}

		public static int InStr(string String1, string String2)
		{
			int idx = String1.IndexOf( String2 );
			return idx + 1;
		}

		public static int InStr(int idx, string String1, string String2)
		{
			--idx; //...this deals with VB string indexing
			idx = String1.IndexOf( String2, idx );
			return idx + 1;
		}

		public static string Trim(string str)
		{
			if (str == null) return string.Empty;
			return str.Trim();
		}

		public static string Replace(string str, int idx, int len, string replace)
		{
			--idx; //...deals with VB string indexing
			string left = (idx < 0) ? string.Empty : str.Substring( 0, idx );
			string right = str.Substring( idx + len );
			return left + replace + right;
		}

		public static int Asc(string str)
		{
			//TODO: Remove this when VB ported code is fully converted.
			return (int)str[ 0 ];
		}
	}
}
