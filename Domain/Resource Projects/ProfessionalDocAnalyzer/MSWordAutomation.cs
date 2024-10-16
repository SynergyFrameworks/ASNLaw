using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace ProfessionalDocAnalyzer
{
    static class MSWordAutomation
    {
        public static void OpenMSWord(string pathFile)
        {
            object oFileName = pathFile;
            object oWord = Activator.CreateInstance(Type.GetTypeFromProgID("Word.Application"));
            oWord.GetType().InvokeMember("Visible", System.Reflection.BindingFlags.SetProperty, null, oWord, new object[] { true });
            object oDocs = oWord.GetType().InvokeMember("Documents", System.Reflection.BindingFlags.GetProperty, null, oWord, null);
            object oDoc = oDocs.GetType().InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, oDocs, new Object[] { oFileName });
            //oDoc.GetType().InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, oDoc, null);
            //oWord.GetType().InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, oWord, null);
            //oWord = null;
        }

        public static bool IsMSWordInstalled()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Winword.exe");
            if (key != null)
                key.Close();

            return key != null;
        }
    }
}
