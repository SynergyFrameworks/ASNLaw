using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.QC
{
    public static class QCParseResultsFields
    {
        // Parse Seg fields
        public static string UID = "UID";
        public static string Number = "Number";
        public static string Rank = "Rank";
        public static string Caption = "Caption";
        public static string SortOrder = "SortOrder";
        public static string Page = "Page";

        // QC fields
        public static string Weight = "Weight";
        public static string Readability = "Readability";
        public static string LongSentences = "LongSentences";
        public static string ComplexWords = "ComplexWords";
        public static string PassiveVoice = "PassiveVoice";
        public static string Adverbs = "Adverbs";
        public static string DictionaryTerms = "DictionaryTerms";

        public static string Words = "Words";
        public static string Sentences = "Sentences";

        public static string TableName = "QCParseResults";

    }
}
