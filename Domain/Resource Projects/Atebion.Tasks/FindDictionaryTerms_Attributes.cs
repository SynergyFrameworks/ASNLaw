using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class FindDictionaryTerms_Attributes
    {
        public const string UserSelectsDictionaryLib = "UserSelectsDictionaryLib";
        public const string UserSelectsDictionaryLib_Caption = "User Selects Dictionary Library?";
        public const string UserSelectsDictionaryLib_ValueOptions = "Yes|No";
        public const string UserSelectsDictionaryLib_Instructions = "Allow the user to select Dictionary Terms, Yes or No?";


        public const string FindWholeWords = "FindWholeWords";
        public const string FindWholeWords_Caption = "Find Whole Words?";
        public const string FindWholeWords_ValueOptions = "Yes|No";
         public const string FindWholeWords_Instructions = "Selecting 'No' will find partial Terms. For example, the Term require will identify 'required', 'requirement', and 'requires'.";

        public const string FindSynonyms = "FindSynonyms";
        public const string FindSynonyms_Caption = "Find Synonyms?";
        public const string FindSynonyms_ValueOptions = "Yes|No";
        public const string FindSynonyms_Instructions = "A synonym is a word or phrase that means exactly or nearly the same.";

        public const string UseDictionaryLibrary = "UseDictionaryLibrary";
        public const string UseDictionaryLibrary_Caption = "Use Dictionary Library";
        public const string UseDictionaryLibrary_ValueOptions = "*"; // Fetch Dictionary Libraries
        public const string UseDictionaryLibrary_Instructions = "";

 

    }
}
