using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcroParser2
{
    static class AcronymsFoundFieldConst
    {
        public static string UID = "UID";
        public static string Acronym = "Acronym";
        public static string xSentence = "Sentence";
        public static string FoundByAlgorithm = "FoundByAlgorithm"; // Ref. Arch. Diagram - e.g. 5.2 = "Find Irregular Acronyms w/out Def.s (5)", 2nd RegExp
        public static string Definition = "Definition";
        public static string SentenceNo = "SentenceNo"; // Int
        public static string SentenceNos = "SentenceNos"; // String
        public static string DefinitionSource = "DefinitionSource"; // Value Options: "None", "Dictionary", "Document"
        public static string Dictionary = "Dictionary"; // If DefinitionSource = "Dictionary", Then the name of the Dictionary/Library that contained the Definition
        public static string Index = "Index"; // Index where the matched text begins within the Sentence
        public static string Length = "Length"; // Is the length of the Acronym (>3) or the Definition + Acronym (3.1) -- (Algorithm Ref. No.)

        // DefinitionSource Value Options:
        public static string DefinitionSource_None = "None";
        public static string DefinitionSource_Dictionary = "Dictionary";
        public static string DefinitionSource_Document = "Document";
       

    }
}
