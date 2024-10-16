using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfessionalDocAnalyzer
{
    public static class ParseResultsFields
    {
        public static string UID = "UID";
        public static string Parameter = "Parameter"; // Not used for Paragraph parsed segments -- Defualt value Null

        public static string Parent = "Parent";
        public static string LineStart = "LineStart"; // Not used for Paragraph parsed segments -- Defualt value -1
        public static string LineEnd = "LineEnd"; // Not used for Paragraph parsed segments -- Defualt value -1
        public static string SectionLength = "SectionLength";
        public static string ColumnStart = "ColumnStart"; // Not used for Paragraph parsed segments -- Defualt value -1
        public static string ColumnEnd = "ColumnEnd"; // Not used for Paragraph parsed segments -- Defualt value -1
        public static string IndexStart = "IndexStart"; // Not used for Paragraph parsed segments -- Defualt value -1
        public static string IndexEnd = "IndexEnd"; // Not used for Paragraph parsed segments -- Defualt value -1
        public static string Number = "Number";
        public static string Caption = "Caption";
        public static string SortOrder = "SortOrder";
        public static string FileName = "FileName";
        public static string Keywords = "Keywords";

        public static string PageSource = "Page";

        public static string NumberLevel = "NumberLevel"; // Optional, for Numerical Hierarchy
        public static string OriginalNumber = "OriginalNumber"; // Optional for Generating Numerical Hierarchy Concatenation

        public static string XMLFile = "ParseResults.xml";

        //public static string CommonConceptsWords = "CCWords"; // used for Docs Explorer tool
        //public static string CommonPhrases = "CPhrases"; // used for Docs Explorer tool
        //public static string CommonPhrasesCount = "CPhrasesCount"; // used for Docs Explorer tool
        //public static string CommonPhrasesUIDs = "CPhrasesUIDs"; // used for Docs Explorer tool

    }
}
