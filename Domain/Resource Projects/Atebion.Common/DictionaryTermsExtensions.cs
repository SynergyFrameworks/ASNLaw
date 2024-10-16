using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atebion.Common
{
    public static class DictionaryTermsExtensions
    {
        public static string SanitizeDictionaryTerm(this string term)
        {
            return term.Replace("'", "''");
        }
    }
}
