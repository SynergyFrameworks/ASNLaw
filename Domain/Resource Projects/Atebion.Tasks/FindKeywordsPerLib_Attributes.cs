using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class FindKeywordsPerLib_Attributes
    {
        public const string UserSelectsKeywordLib = "UserSelectsKeywordLib";
        public const string UserSelectsKeywordLib_Caption = "User Selects Keyword Library?";
        public const string UserSelectsKeywordLib_ValueOptions = "Yes|No";
        public const string UserSelectsKeywordLib_Instructions = "Allow the user to select Keywords, Yes or No?";


        public const string FindWholeWords = "FindWholeWords";
        public const string FindWholeWords_Caption = "Find Whole Words?";
        public const string FindWholeWords_ValueOptions = "Yes|No";
        public const string FindWholeWords_Instructions = "Selecting 'No' will find partial Keywords. For example, the keyword require will identify 'required', 'requirement', and 'requires'.";

        public const string UseKeywordLibrary = "UseKeywordLibrary";
        public const string UseKeywordLibrary_Caption = "Use Keyword Library";
        public const string UseKeywordLibrary_ValueOptions = "*"; // Fetch Keyword Libraries
        public const string UseKeywordLibrary_Instructions = "Select a Keyword Group that will be used for the analysis.";
    }
}
