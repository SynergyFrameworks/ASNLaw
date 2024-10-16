using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.ConceptAnalyzer
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

        public static string ConceptsWords = "Concepts"; // used for Concept Analyzer tool

        public static string Dictionary = "Dictionary";
        public static string DictionaryItems = "DicItems";
        public static string DictionaryDefinitions = "DicDefs";
        public static string DictionaryCategory = "DicCat";
        public static string Weight = "Weight";  //Weighted value
        


 

        public static string XMLFile = "ParseResults.xml";

    }
}
