//
// jeff.key@sliver.com
//
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Atebion.RichTextExtra
{
	/// <summary>
	/// Creates a balloon tooltip on a TextBox.
	/// </summary>
	/// <remarks>Only works on Windows XP and greater.</remarks>
	public class TextBoxBallonTip
	{
		#region BalloonTipIncons enum

		public enum BallonTipIcons
		{
			None,
			Info,
			Warning,
			Error
		}

		#endregion

		[DllImport("user32.dll", EntryPoint="SendMessageA")]
		private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, ref EDITBALLOONTIP lParam);
		private const int ECM_FIRST = 0x1500;
		private const int EM_SHOWBALLOONTIP = ECM_FIRST + 3;

		[StructLayout(LayoutKind.Sequential)]
		private struct EDITBALLOONTIP
		{
			public int cbStruct;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszTitle;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszText;
			public int ttiIcon;

			public EDITBALLOONTIP(string title, string text, int icon)
			{
				cbStruct = Marshal.SizeOf(typeof(EDITBALLOONTIP));
				pszTitle = title;
				pszText = text;
				ttiIcon = icon;
			}
		}

        public static void ShowBalloonTip(Control textBox, string title, string text, ToolTipIcon icon)
		{
			if (Environment.OSVersion.Version.Major >= 5 || Environment.OSVersion.Version.Minor >= 1)
			{
			//	EDITBALLOONTIP stTip = new EDITBALLOONTIP(title, text, (int)icon);
			//	SendMessage(textBox.Handle, EM_SHOWBALLOONTIP, 0, ref stTip);

                ToolTip ttp = new ToolTip();
                ttp.SetToolTip(textBox, title);
                ttp.UseFading = true;
                ttp.IsBalloon = false;
                ttp.ToolTipTitle = title;
                ttp.ShowAlways = true;
                ttp.ToolTipIcon = icon;
                ttp.Show(text, textBox, 300);
			}
		}

        public static void ShowBalloonTip(RichTextBox textBox, string title, string text)
		{
			// NOTE:  Last param isn't used.
            ShowBalloonTip(textBox, title, text, ToolTipIcon.None);
		}
	}
}