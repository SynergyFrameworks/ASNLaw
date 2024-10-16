using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class GenerateRAM_Attributes
    {

        public const string UseRAMTemplate = "UseRAMTemplate";
        public const string UseRAMTemplate_Caption = "Use RAM Template";
        public const string UseRAMTemplate_ValueOptions = "*"; // Fetch Templates
        public const string UseRAMTemplate_Instructions = "";

        public const string FindSynonyms = "FindSynonyms";
        public const string FindSynonyms_Caption = "Find Synonyms?";
        public const string FindSynonyms_ValueOptions = "Yes|No";
        public const string FindSynonyms_Instructions = "A synonym is a word or phrase that means exactly or nearly the same.";

        public const string UseColor = "UseColor";
        public const string UseColor_Caption = "Use Color";
        public const string UseColor_ValueOptions = "Yes|No";
        public const string UseColor_Instructions = "Display backgroup color for assignment's cells and gray background for rows without found dictionary terms.";


    }
}
