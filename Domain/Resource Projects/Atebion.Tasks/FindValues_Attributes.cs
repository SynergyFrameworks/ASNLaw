using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class FindValues_Attributes
    {
        public const string FindCSVs = "FindCSVs";
        public const string FindCSVs_Caption = "Find Comma-Separated Values (CSV)";
        public const string FindCSVs_ValueOptions = "*";
        public const string FindCSVs_Instructions = "Enter Comma-Separated Values (CSV)";

        public const string FindWholeWords = "FindWholeWords";
        public const string FindWholeWords_Caption = "Find Whole Words?";
        public const string FindWholeWords_ValueOptions = "Yes|No";
        public const string FindWholeWords_Instructions = "Selecting 'No' will find partial Terms. For example, the Term require will identify 'required', 'requirement', and 'requires'.";

    }
}
