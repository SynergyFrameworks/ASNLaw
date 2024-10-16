using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProfessionalDocAnalyzer
{
    public static class OutLookMgr
    {
        public static bool isOutLookRunning()
        {
            //int procCount = 0;
            Process[] processlist = Process.GetProcessesByName("OUTLOOK");
            
            if (processlist.Length > 0)
                return true;

            return false;

            //foreach (Process theprocess in processlist)
            //{
            //    procCount++;
            //}
            //if (procCount > 0)
            //{
            //    return true;
            //}
        }
    }
}
