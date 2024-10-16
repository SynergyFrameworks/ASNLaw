using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public  static class ProcessObject_Level
    {
        // ---- Warning -- Do NOT change squence order

        public static string Base_Level = string.Concat(ProcessObject.Parse, "|", ProcessObject.CompareDocsDiff, "|", ProcessObject.AcroSeeker, "|", ProcessObject.CreateXRefMatrix, "|", ProcessObject.XRefMatrices);

        public static string Parse_Children = string.Concat(ProcessObject.FindKeywordsPerLib, "|", ProcessObject.FindConcepts, "|", ProcessObject.CompareDocsConcepts, "|", ProcessObject.FindDictionaryTerms, "|", ProcessObject.CompareDocsDictionary, "|", ProcessObject.ReadabilityTest, "|",  ProcessObject.DeepAnalyze, "|", ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateRAMRpt, "|", ProcessObject.GenerateReport);

        public static string Parse_Defualt_Children = string.Concat(ProcessObject.FindKeywordsPerLib, "|", ProcessObject.DeepAnalyze, "|", ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateReport);

        public static string CompareDocsConcepts_Children = string.Concat(ProcessObject.DisplayAnalysisResults);

        public static string CompareDocsDictionary_Children = string.Concat(ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateReport);
        
        public static string FindKeywordsPerLib_Children = string.Concat(ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateReport);

        public static string ReadabilityTest_Children = string.Concat(ProcessObject.DisplayAnalysisResults);

        public static string FindKeywordsPerLib_Defualt_Children = string.Concat(ProcessObject.DeepAnalyze, "|", ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateReport);

        public static string FindConcepts_Children = string.Concat(ProcessObject.DisplayAnalysisResults);

        public static string FindDictionaryTerms_Children = string.Concat(ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateReport);

        public static string FindValues_Children = string.Concat(ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateReport);

        public static string FindMetaData_Children = string.Concat(ProcessObject.DeepAnalyze, "|", ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateReport);

        public static string DisplayAnalysisResults_Defualt_Children = ProcessObject.DeepAnalyze;

        public static string DeepAnalyze_Children = string.Concat(ProcessObject.DisplayAnalysisResults);




            public static string Summary_Children = string.Empty;

            public static string CompareDocsDiff_Children = string.Empty;

            public static string Txt2Speech_Children = string.Empty;

            public static string AcroSeeker_Children = string.Empty;

       



    }
}
