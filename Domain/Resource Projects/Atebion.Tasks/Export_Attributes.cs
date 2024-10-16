using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class GenerateReport_Attributes
    {
        public static string ReportFileType = "ReportFileType";
        public static string ReportFileType_Caption = "Report File Type?";
        public static string ReportFileType_ValueOptions = "Excel";
        public static string ReportFileType_Instructions = "Select a report file Type. Templates are supported with Excel.";

        public static string UseExcelTemplate = "UseExcelTemplate";
        public static string UseExcelTemplate_Caption = "Use Excel Template";
        public static string UseExcelTemplate_ValueOptions = "*"; // Fetch Excel Templates
        public static string UseExcelTemplate_Instructions = "Select an Excel Template.";

        // Hidden Attribute in the Task Workflow Configuration dialog window
        public static string UseFor = "UseFor";
        public static string UserFor_Caption = "Use for Action?";
        public static string UseFor_ValueOptions = string.Concat(ReportFor.CompareConcepts, "|", ReportFor.CompareDictionary, "|", ReportFor.DeepAnalysis, "|", ReportFor.ParseConcepts, "|", ReportFor.ParseDictionary, "|", ReportFor.ParseFARsDFARs, "|", ReportFor.ParseKeywords);
        public static string UseFor_Instructions = "Hidden Attribute in the Task Workflow Configuration dialog window";

        public static string UseWeightColors4Report = "UseWeightColors4Report";
        public static string UseWeightColors4Report_Caption = "Use Weight Colors";
        public static string UseWeightColors4Report_ValueOptions = "Yes|No";
        public static string UseWeightColors4Report_Instructions = "Weighted colors provide a quick visual representation of weighted values.";
       
    }
}
