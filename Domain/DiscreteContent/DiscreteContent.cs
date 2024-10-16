using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.DiscreteContent
{
    public class DiscreteContent
    {

        public string[] FindURLs(string InputText)
        {
            List<string> urls = new List<string>();

            var parser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Explanation Pattern:
            // \b       -matches a word boundary (spaces, periods..etc)
            // (?:      -define the beginning of a group, the ?: specifies not to capture the data within this group.
            // https?://  - Match http or https (the '?' after the "s" makes it optional)
            // |        -OR
            // www\.    -literal string, match www. (the \. means a literal ".")
            // )        -end group
            // \S+      -match a series of non-whitespace characters.
            // \b       -match the closing word boundary.

            foreach (Match m in parser.Matches(InputText))
            {
                urls.Add(m.Value);
            }

            return urls.ToArray();

        }

        public string[] FindEmails(string InputText)
        {
            List<string> emails = new List<string>();

            var parser = new Regex(@"\b[A-Z0-9._-]+@[A-Z0-9][A-Z0-9.-]{0,61}[A-Z0-9]\.[A-Z.]{2,6}\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (Match m in parser.Matches(InputText))
            {
                emails.Add(m.Value);
            }

            return emails.ToArray();
        }

        public string[] FindDates(string InputText)
        {
            List<string> emails = new List<string>();

            var parser = new Regex(@"^(?:(1[0-2]|0?[1-9])/(3[01]|[12][0-9]|0?[1-9])|(3[01]|[12][0-9]|0?[1-9])/(1[0-2]|0?[1-9]))/(?:[0-9]{2})?[0-9]{2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (Match m in parser.Matches(InputText))
            {
                emails.Add(m.Value);
            }

            return emails.ToArray();
        }
    }
}
