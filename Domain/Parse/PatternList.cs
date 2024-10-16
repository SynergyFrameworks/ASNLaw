namespace Domain.Parse
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class PatternList
    {

        public class PatternDef
        {
            public readonly string Pattern;

            public bool MatchesLike(string str) { return LikeOperator.LikeOperator.LikeString(str, Pattern); }

            public bool Matches(string str) { return _regex.IsMatch(str); }

            public bool IsNonTerminal(string str)
            {
                if (_isNonTerminal)
                {
                    return str.Length > _limit;
                }
                if (_isCurrency)
                {
                    return str[0] == '$';
                }
                return false;
            }

            public PatternDef(string pattern, bool isNonTerminal, bool isCurrency, int limit)
            {
                Pattern = pattern;

                _isNonTerminal = isNonTerminal;
                _isCurrency = isCurrency;
                _limit = limit;

                _regex = new Regex(convert(pattern), RegexOptions.Compiled);
            }
            private bool _isNonTerminal;
            private bool _isCurrency;
            private int _limit;

            private Regex _regex;
        }

        public int Length => _patterns.Count;

        public PatternDef this[int idx] => _patterns[idx];

        public PatternList(string[] lines)
        {
            int len = lines.Length;

            _patterns = new List<PatternDef>(len);

            for (int i = 0; i < len; ++i)
            {
                string pattern = lines[i].Trim();

                if (pattern.Length == 0) continue;

                bool is_currency = (pattern == _currencyPattern);

                int terminal_idx = (!is_currency) ? Array.IndexOf(_nonTerminalPatterns, pattern) : -1;
                bool is_non_terminal = terminal_idx >= 0;

                int limit = (is_non_terminal) ? _limits[terminal_idx] : 0;

                _patterns.Add(new PatternDef(pattern, is_non_terminal, is_currency, limit));
            }
        }

        private List<PatternDef> _patterns;
        private static string _currencyPattern = "*.##";
        private static string[] _nonTerminalPatterns = { "*[a-z].", "*[A-Z].", "*[a-z])", "*([A-Z])", "*[A-Z])" }; //...whether something is non-terminal should be part of the individual pattern loaded, but that requires a change to the format of the input file. This is just for testing.
        private static int[] _limits = { 5, 5, 5, 7, 7 };

        private static string convert(string pat) //...converts to Regex pattern
        {
            int state = 0;
            int idx = -1, limit = pat.Length - 1;
            const char NUL = '\0';

            char get_next()
            {
                if (idx < limit)
                {
                    return pat[++idx];
                }
                return NUL;
            }

            char curr_ch = get_next();

            void advance()
            {
                curr_ch = get_next();
            }

            StringBuilder sb = new StringBuilder("^");

            bool done = false;

            do
            {
                switch (state)
                {
                    case 0: //...general case; look for patterns and literal characters
                        switch (curr_ch)
                        {
                            case NUL: sb.Append('$'); done = true; break;
                            case '#': sb.Append(@"\d"); advance(); break;
                            case '.': sb.Append(@"\."); advance(); break;
                            case '*': sb.Append(@".*"); advance(); break;
                            case '?': sb.Append(@"."); advance(); break;
                            case '(': sb.Append(@"\("); advance(); break;
                            case ')': sb.Append(@"\)"); advance(); break;
                            case '[': sb.Append(@"["); state = 1; advance(); break;
                          //  case '^': sb.Append(@"*"); advance(); break;
                            default: sb.Append(curr_ch); advance(); break;
                        }
                        break;

                    case 1: //...looking for character set; starting, looking for first char in set
                        switch (curr_ch)
                        {
                            case '!': sb.Append('^'); state = 2; advance(); break;
                            case ']':
                            case NUL: throw new Exception($"PatternList: Expecting range! Pattern: '{pat}', index: {idx}, partial: {sb}.");
                            default: sb.Append(curr_ch); state = 3; advance(); break;
                        }
                        break;

                    case 2: //...looking for character set; expecting first character
                        switch (curr_ch)
                        {
                            case ']':
                            case NUL: throw new Exception($"PatternList: Expecting range! Pattern: '{pat}', index: {idx}, partial: {sb}.");
                            default: sb.Append(curr_ch); state = 3; advance(); break;
                        }
                        break;

                    case 3: //...collecting character set
                        switch (curr_ch)
                        {
                            case ']': sb.Append(']'); state = 0; advance(); break;
                            case NUL: throw new Exception($"PatternList: Expecting range! Pattern: '{pat}', index: {idx}, partial: {sb}.");
                            default: sb.Append(curr_ch); advance(); break;
                        }
                        break;
                }
            }
            while (!done);

            return sb.ToString();
        }
    }
}
