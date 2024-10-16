using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class Parse_Attributes
    {
        public static string UseDefaultParseAnalysis = "UseDefaultParseAnalysis";
        public static string UseDefaultParseAnalysis_Caption = "Use Default Parse Analysis?";
        public static string UseDefaultParseAnalysis_ValueOptions = "Yes|No";
        public static string UseDefaultParseAnalysis_Instructions = "Default Parse Analysis is required if you want to use the parse results in the Matrix Builder. – Default Parse Analysis does supports only Keywords, not Dictionaries and Concepts.";

        public static string DocQty = "DocQty";
        public static string DocQty_Caption = "Quantity of Documents";
        public static string DocQty_ValueOptions = "1|2|2 or more";
        public static string DocQty_Instructions = "Quantity of Documents depends on the analysis you want to run. For example, for a Federal RFP, you may want to split the RPF into separate documents for each section (e.g. C, L, & M) and parse each of these documents at once.";

        public static string ParseType = "ParseType";
        public static string ParseType_Caption = "Parse Type";
        public static string ParseType_ValueOptions = "Legal|Paragraph";
        public static string ParseType_Instructions = "Legal documents are documents that have numbered content, such as most Federal RFPs.";

        public static string Show_ParseType = "ShowParseType";
        public static string Show_ParseType_Caption = "Show Parse Type?";
        public static string Show_ParseType_ValueOptions = "Yes|No";
        public static string Show_ParseType_Instructions = "Allows the user to change the Parse Type (Legal or Paragraph)";

        // Use Attribute for only Legal parsing type
        public static string NumericalHierarchyConcatenation = "NumericalHierarchyConcatenation";
        public static string NumericalHierarchyConcatenation_Caption = "Use Numerical Hierarchy Concatenation?";
        public static string NumericalHierarchyConcatenation_ValueOptions = "Yes|No";
        public static string NumericalHierarchyConcatenation_Instructions = "Use for only Legal parsing type. Example of Numerical Hierarchy Concatenation (NHC), parent number is '1.1' and child is 'A', then the NHC value is '1.1 A'.";

        public static string PrefixNumber = "PrefixNumber";
        public static string PrefixNumber_Caption = "Prefix Number";
        public static string PrefixNumber_ValueOptions = "*"; // If this Attribute exists, User enters Prefix, e.g. "L", "C", "M", ...
        public static string PrefixNumber_Instructions = "Prefix the parse segment/paragraph number. For example, if you are working on a Federal RFP SOW, you may want to prefix with the letter 'C'.";
       


    }
}
