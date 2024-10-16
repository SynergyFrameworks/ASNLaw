using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfessionalDocAnalyzer
{
    public static class AnalysisResultsType
    {
        public enum Selection
        {
              Logic_Segments = 0,
              Paragraph_Segments = 1,
              Logic_Sentences = 2,
              Paragraph_Sentences = 3,
        }

        public enum SearchType
        {
            None = 0,
            Keywords = 1,     
            Concepts = 2,
            Dictionary = 3,
            FAR_DFAR = 4
        }
    }
}
