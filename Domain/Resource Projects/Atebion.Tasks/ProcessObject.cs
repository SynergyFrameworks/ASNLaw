using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class ProcessObject
    {
        // 1st Level
        public  const string Parse = "Parse";
        public  const string Summary = "Generate Summary";
        public  const string CompareDocsDiff = "Compare Documents Differences";
        public  const string Txt2Speech = "Read Document";
        public  const string AcroSeeker = "Identify and Validate Acronyms";
        public  const string ReadabilityTest = "Readability Test";
        public const string CompareDocsDictionary = "Compare Docs per Dictionary";
        public const string CompareDocsConcepts = "Compare Docs per Concepts";
        public const string CreateXRefMatrix = "Create Cross Reference Matrix";
        public const string OpenXRefMatrix = "Open Cross Reference Matrix";
        public const string XRefMatrices = "Go to Cross Reference Matrices";


        // 2nd Level
        public  const string DeepAnalyze = "Deep Analyze";
      //  public  const string GenerateXRefMatrix = "Generate Cross-Reference Matrix";
        public const string GenerateReport = "Generate Report";
        

        // 2 Level
        public  const string FindValues = "Find Values";
        public  const string FindKeywordsPerLib = "Find Keywords per Library";
        public  const string FindConcepts = "Find Concepts";
        public  const string FindDictionaryTerms = "Find Dictionary Terms";
        public  const string FindMetaData = "Find Metadata";
        public  const string FindFARs = "Find FARs and DFARs";
        public  const string GenerateRAMRpt = "Generate Responsibility Assignment Matrix";

        // 2nd or 3rd Levels
        public  const string DisplayAnalysisResults = "DisplayAnalysisResults";


        // 3rd or 4th Levels
      //  public  const string Export = "Export";

    }
}
