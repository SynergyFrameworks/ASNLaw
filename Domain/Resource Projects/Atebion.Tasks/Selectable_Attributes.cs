using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class Selectable_Attributes
    {
        public static string DocQty = "DocQty";
        public static string DocQty_Caption = "Documents Qty";
        public static string DocQty_ValueOptions = "1|2|2 or more";

        public static string ParseType = "ParseType";
        public static string ParseType_Caption = "Parse Type";
        public static string ParseType_ValueOptions = "Legal|Paragraph";

        public static string Show_ParseType = "ShowParseType";
        public static string Show_ParseType_Caption = "Show Parse Type?";
        public static string Show_ParseType_ValueOptions = "Yes|No";

        public static string UseDefaultParseAnalysis = "UseDefaultParseAnalysis";
        public static string UseDefaultParseAnalysis_Caption = "Use Default Parse Analysis?";
        public static string UseDefaultParseAnalysis_ValueOptions = "Yes|No";

        //public static string Identify = "Identify";
        //public static string Identify_Caption = "Identify";
        //public static string Identify_ValueOptions = "None|Keywords|Concepts|Dictionary Terms";

        public static string UserSelectsKeywordLib = "UserSelectsKeywordLib";
        public static string UserSelectsKeywordLib_Caption = "User Selects Keyword Library?";
        public static string UserSelectsKeywordLib_ValueOptions = "Yes|No";

        public static string FindWholeWords = "FindWholeWords";
        public static string FindWholeWords_Caption = "Find Whole Words?";
        public static string FindWholeWords_ValueOptions = "Yes|No";

        public static string FindCommaDelimitedValues = "CommaDelimitedValues";
        public static string FindCommaDelimitedValues_Caption = "Comma Delimited Values";
        public static string FindCommaDelimited_ValueOptions = "?"; // Denotes user input

        public static string UseKeywordLibrary = "UseKeywordLibrary";
        public static string UseKeywordLibrary_Caption = "Use Keyword Library";
        public static string UseKeywordLibrary_ValueOptions = "*"; // Fetch Keyword Libraries

        public static string ShowAnalysisResults = "ShowAnalysisResults";
        public static string ShowAnalysisResults_Caption = "Show Analysis Results?";
        public static string ShowAnalysisResults_ValueOptions = "Yes|No";

        public static string ExportFileType = "ExportFileType";
        public static string ExportFileType_Caption = "Export to...";
        public static string ExportFileType_ValueOptions = "Excel|MS Word|HTML";

        public static string UseExcelTemplate = "UseExcelTemplate";
        public static string UseExcelTemplate_Caption = "Use Excel Template";
        public static string UseExcelTemplate_ValueOptions = "*"; // Fetch Excel Templates



    }
}
