﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;

namespace Atebion.DiffSxS
{
    class DiffModHTML
    {
        private StringBuilder content;
        //   private string oldText, newText;
        private string[] oldWords, newWords;
        Dictionary<string, List<int>> wordIndices;
        private string[] specialCaseOpeningTags = new string[] { "<strong[\\>\\s]+", "<b[\\>\\s]+", "<i[\\>\\s]+", "<big[\\>\\s]+", "<small[\\>\\s]+", "<u[\\>\\s]+", "<sub[\\>\\s]+", "<sup[\\>\\s]+", "<strike[\\>\\s]+", "<s[\\>\\s]+" };
        private string[] specialCaseClosingTags = new string[] { "</strong>", "</b>", "</i>", "</big>", "</small>", "</u>", "</sub>", "</sup>", "</strike>", "</s>" };

        private const string TAB = "\t";
        private const string QUOTE = "\"";

        /// <summary>
        /// Initializes a new instance of the <see cref="Diff"/> class.
        /// </summary>
        /// <param name="oldText">The old text.</param>
        /// <param name="newText">The new text.</param>
        public DiffModHTML(string oldText, string newText)
        {
            this.oldText = oldText;
            this.newText = newText;

            this.content = new StringBuilder();
        }

        public DiffModHTML()
        {

            this.content = new StringBuilder();
        }



        private string oldText = string.Empty;
        public string OldText
        {
            get { return oldText; }
            set { oldText = value; }
        }

        private string newText = string.Empty;
        public string NewText
        {
            get { return newText; }
            set { newText = value; }
        }


        #region Generation

        public string GenerateWholeDiffMod(string oldText, string newText)
        {
            this.oldText = oldText;
            this.newText = newText;

            return GenerateWholeDiffMod();
        }

        public string GenerateWholeDiffMod()
        {
            StringBuilder wSB = new StringBuilder();
            wSB.AppendLine(string.Concat("<!DOCTYPE HTML PUBLIC ", QUOTE, "-//W3C//DTD HTML 4.0 Transitional//EN", QUOTE, ">"));
            wSB.AppendLine("<html>");
            wSB.AppendLine(string.Concat(TAB, "<head>"));
            wSB.AppendLine(string.Concat(TAB, TAB, "<title>Atebion LLC, Diff Modified</title>"));
            wSB.AppendLine(string.Concat(TAB, TAB, "<style type=", QUOTE, "text/css", QUOTE, ">"));
            wSB.AppendLine(" ");
            string diffStyle = GetStyles();
            wSB.Append(diffStyle);
            wSB.AppendLine(" ");
            wSB.AppendLine(string.Concat(TAB, TAB, "</style>"));
            wSB.AppendLine(string.Concat(TAB, "</head>"));
            wSB.AppendLine(" ");
            wSB.AppendLine(string.Concat(TAB, TAB, "<body>"));
            wSB.AppendLine(" ");
            string diffHTML = Build();
            wSB.Append(diffHTML);
            wSB.AppendLine(" ");
            wSB.AppendLine(string.Concat(TAB, TAB, "</body>"));
            wSB.AppendLine(string.Concat(TAB, "</html>"));

            return wSB.ToString();
        }



        #endregion


        public string GetStyles()
        {
            StringBuilder sbS = new StringBuilder();

            sbS.AppendLine(@"/***************** Diff related styles *****************/");
            sbS.AppendLine(" ");
            sbS.AppendLine("ins {");
            sbS.AppendLine(TAB + " font-family: verdana, helvetica, arial, sans-serif;");
            sbS.AppendLine(TAB + " background-color: #cfc;");
            sbS.AppendLine(TAB + " text-decoration: underline;");
            sbS.AppendLine("}");
            sbS.AppendLine(" ");
            sbS.AppendLine("del {");
            sbS.AppendLine(TAB + " font-family: verdana, helvetica, arial, sans-serif;");
            sbS.AppendLine(TAB + " color:black;");
            sbS.AppendLine(TAB + " background-color:#FEC8C8;");
            sbS.AppendLine("}");
            sbS.AppendLine(" ");
            sbS.AppendLine("ins.mod {");
            sbS.AppendLine(TAB + " font-family: verdana, helvetica, arial, sans-serif;");
            sbS.AppendLine(TAB + " background-color: #FFE1AC;");
            sbS.AppendLine("}");
            sbS.AppendLine(" ");
            sbS.AppendLine(".diffmod {");
            sbS.AppendLine(TAB + " font-family: verdana, helvetica, arial, sans-serif;");
            sbS.AppendLine("}");
            sbS.AppendLine(" ");


            return sbS.ToString();
        }

        /// <summary>
        /// Builds the HTML diff output
        /// </summary>
        /// <returns>HTML diff markup</returns>
        public string Build()
        {
            content.Clear();

            this.SplitInputsToWords();

            this.IndexNewWords();

            var operations = this.Operations();

            foreach (var item in operations)
            {
                this.PerformOperation(item);
            }

            return this.content.ToString();
        }

        private void IndexNewWords()
        {
            this.wordIndices = new Dictionary<string, List<int>>();
            for (int i = 0; i < this.newWords.Length; i++)
            {
                string word = this.newWords[i];

                if (this.wordIndices.ContainsKey(word))
                {
                    this.wordIndices[word].Add(i);
                }
                else
                {
                    this.wordIndices[word] = new List<int>();
                    this.wordIndices[word].Add(i);
                }
            }
        }

        private string CleanHTML2(string txt)
        {
            // call the normal HtmlEncode first
            char[] chars = System.Web.HttpUtility.HtmlEncode(txt).ToCharArray();
            StringBuilder encodedValue = new StringBuilder();
            foreach (char c in chars)
            {
                if ((int)c > 127) // above normal ASCII
                    encodedValue.Append("&#" + (int)c + ";");
                else
                    encodedValue.Append(c);
            }
            return encodedValue.ToString();

        }

        private void SplitInputsToWords()
        {
            this.oldText = CleanHTML2(this.oldText);
            this.oldWords = ConvertHtmlToListOfWords(this.Explode(this.oldText));

            this.newText = CleanHTML2(this.newText);
            this.newWords = ConvertHtmlToListOfWords(this.Explode(this.newText));
        }

        private string[] ConvertHtmlToListOfWords(string[] characterString)
        {
            Mode mode = Mode.character;
            string current_word = String.Empty;
            List<string> words = new List<string>();

            foreach (var character in characterString)
            {
                switch (mode)
                {
                    case Mode.character:

                        if (this.IsStartOfTag(character))
                        {
                            if (current_word != String.Empty)
                            {
                                words.Add(current_word);
                            }

                            current_word = "<";
                            mode = Mode.tag;
                        }
                        else if (Regex.IsMatch(character, "\\s"))
                        {
                            if (current_word != String.Empty)
                            {
                                words.Add(current_word);
                            }
                            current_word = character;
                            mode = Mode.whitespace;
                        }
                        else
                        {
                            current_word += character;
                        }

                        break;
                    case Mode.tag:

                        if (this.IsEndOfTag(character))
                        {
                            current_word += ">";
                            words.Add(current_word);
                            current_word = "";

                            if (IsWhiteSpace(character))
                            {
                                mode = Mode.whitespace;
                            }
                            else
                            {
                                mode = Mode.character;
                            }
                        }
                        else
                        {
                            current_word += character;
                        }

                        break;
                    case Mode.whitespace:

                        if (this.IsStartOfTag(character))
                        {
                            if (current_word != String.Empty)
                            {
                                words.Add(current_word);
                            }
                            current_word = "<";
                            mode = Mode.tag;
                        }
                        else if (Regex.IsMatch(character, "\\s"))
                        {
                            current_word += character;
                        }
                        else
                        {
                            if (current_word != String.Empty)
                            {
                                words.Add(current_word);
                            }

                            current_word = character;
                            mode = Mode.character;
                        }

                        break;
                    default:
                        break;
                }


            }
            if (current_word != string.Empty)
            {
                words.Add(current_word);
            }

            return words.ToArray();
        }

        private bool IsStartOfTag(string val)
        {
            return val == "<";
        }

        private bool IsEndOfTag(string val)
        {
            return val == ">";
        }

        private bool IsWhiteSpace(string value)
        {
            return Regex.IsMatch(value, "\\s");
        }

        private string[] Explode(string value)
        {
            return Regex.Split(value, "");
        }

        private void PerformOperation(Operation operation)
        {
            switch (operation.Action)
            {
                case Action.equal:
                    this.ProcessEqualOperation(operation);
                    break;
                case Action.delete:
                    this.ProcessDeleteOperation(operation, "diffdel");
                    break;
                case Action.insert:
                    this.ProcessInsertOperation(operation, "diffins");
                    break;
                case Action.none:
                    break;
                case Action.replace:
                    this.ProcessReplaceOperation(operation);
                    break;
                default:
                    break;
            }
        }

        private void ProcessReplaceOperation(Operation operation)
        {
            this.ProcessDeleteOperation(operation, "diffmod");
            this.ProcessInsertOperation(operation, "diffmod");
        }

        private void ProcessInsertOperation(Operation operation, string cssClass)
        {
            this.InsertTag("ins", cssClass, this.newWords.Where((s, pos) => pos >= operation.StartInNew && pos < operation.EndInNew).ToList());
        }

        private void ProcessDeleteOperation(Operation operation, string cssClass)
        {
            var text = this.oldWords.Where((s, pos) => pos >= operation.StartInOld && pos < operation.EndInOld).ToList();
            this.InsertTag("del", cssClass, text);
        }

        private void ProcessEqualOperation(Operation operation)
        {
            var result = this.newWords.Where((s, pos) => pos >= operation.StartInNew && pos < operation.EndInNew).ToArray();
            this.content.Append(String.Join("", result));
        }


        /// <summary>
        /// This method encloses words within a specified tag (ins or del), and adds this into "content", 
        /// with a twist: if there are words contain tags, it actually creates multiple ins or del, 
        /// so that they don't include any ins or del. This handles cases like
        /// old: '<p>a</p>'
        /// new: '<p>ab</p><p>c</b>'
        /// diff result: '<p>a<ins>b</ins></p><p><ins>c</ins></p>'
        /// this still doesn't guarantee valid HTML (hint: think about diffing a text containing ins or
        /// del tags), but handles correctly more cases than the earlier version.
        /// 
        /// P.S.: Spare a thought for people who write HTML browsers. They live in this ... every day.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="cssClass"></param>
        /// <param name="words"></param>
        private void InsertTag(string tag, string cssClass, List<string> words)
        {
            while (true)
            {
                if (words.Count == 0)
                {
                    break;
                }

                var nonTags = ExtractConsecutiveWords(words, x => !this.IsTag(x));

                string specialCaseTagInjection = string.Empty;
                bool specialCaseTagInjectionIsBefore = false;

                if (nonTags.Length != 0)
                {
                    string text = this.WrapText(string.Join("", nonTags), tag, cssClass);

                    this.content.Append(text);
                }
                else
                {
                    // Check if strong tag

                    if (this.specialCaseOpeningTags.FirstOrDefault(x => Regex.IsMatch(words[0], x)) != null)
                    {
                        specialCaseTagInjection = "<ins class='mod'>";
                        if (tag == "del")
                        {
                            words.RemoveAt(0);
                        }
                    }
                    else if (this.specialCaseClosingTags.Contains(words[0]))
                    {
                        specialCaseTagInjection = "</ins>";
                        specialCaseTagInjectionIsBefore = true;
                        if (tag == "del")
                        {
                            words.RemoveAt(0);
                        }
                    }

                }

                if (words.Count == 0 && specialCaseTagInjection.Length == 0)
                {
                    break;
                }

                if (specialCaseTagInjectionIsBefore)
                {
                    this.content.Append(specialCaseTagInjection + String.Join("", this.ExtractConsecutiveWords(words, x => this.IsTag(x))));
                }
                else
                {
                    this.content.Append(String.Join("", this.ExtractConsecutiveWords(words, x => this.IsTag(x))) + specialCaseTagInjection);
                }
            }
        }

        private string WrapText(string text, string tagName, string cssClass)
        {
            return string.Format("<{0} class='{1}'>{2}</{0}>", tagName, cssClass, text);
        }

        private string[] ExtractConsecutiveWords(List<string> words, Func<string, bool> condition)
        {
            int? indexOfFirstTag = null;

            for (int i = 0; i < words.Count; i++)
            {
                string word = words[i];

                if (!condition(word))
                {
                    indexOfFirstTag = i;
                    break;
                }
            }

            if (indexOfFirstTag != null)
            {
                var items = words.Where((s, pos) => pos >= 0 && pos < indexOfFirstTag).ToArray();
                if (indexOfFirstTag.Value > 0)
                {
                    words.RemoveRange(0, indexOfFirstTag.Value);
                }
                return items;
            }
            else
            {
                var items = words.Where((s, pos) => pos >= 0 && pos <= words.Count).ToArray();
                words.RemoveRange(0, words.Count);
                return items;
            }
        }

        private bool IsTag(string item)
        {
            bool isTag = IsOpeningTag(item) || IsClosingTag(item);
            return isTag;
        }

        private bool IsOpeningTag(string item)
        {
            return Regex.IsMatch(item, "^\\s*<[^>]+>\\s*$");
        }

        private bool IsClosingTag(string item)
        {
            return Regex.IsMatch(item, "^\\s*</[^>]+>\\s*$");
        }


        private List<Operation> Operations()
        {
            int positionInOld = 0, positionInNew = 0;
            List<Operation> operations = new List<Operation>();

            var matches = this.MatchingBlocks();

            matches.Add(new Match(this.oldWords.Length, this.newWords.Length, 0));

            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];

                bool matchStartsAtCurrentPositionInOld = (positionInOld == match.StartInOld);
                bool matchStartsAtCurrentPositionInNew = (positionInNew == match.StartInNew);

                Action action = Action.none;

                if (matchStartsAtCurrentPositionInOld == false
                    && matchStartsAtCurrentPositionInNew == false)
                {
                    action = Action.replace;
                }
                else if (matchStartsAtCurrentPositionInOld == true
                    && matchStartsAtCurrentPositionInNew == false)
                {
                    action = Action.insert;
                }
                else if (matchStartsAtCurrentPositionInOld == false
                    && matchStartsAtCurrentPositionInNew == true)
                {
                    action = Action.delete;
                }
                else // This occurs if the first few words are the same in both versions
                {
                    action = Action.none;
                }

                if (action != Action.none)
                {
                    operations.Add(
                        new Operation(action,
                            positionInOld,
                            match.StartInOld,
                            positionInNew,
                            match.StartInNew));
                }

                if (match.Size != 0)
                {
                    operations.Add(new Operation(
                        Action.equal,
                        match.StartInOld,
                        match.EndInOld,
                        match.StartInNew,
                        match.EndInNew));

                }

                positionInOld = match.EndInOld;
                positionInNew = match.EndInNew;
            }

            return operations;

        }

        private List<Match> MatchingBlocks()
        {
            List<Match> matchingBlocks = new List<Match>();
            this.FindMatchingBlocks(0, this.oldWords.Length, 0, this.newWords.Length, matchingBlocks);
            return matchingBlocks;
        }


        private void FindMatchingBlocks(int startInOld, int endInOld, int startInNew, int endInNew, List<Match> matchingBlocks)
        {
            var match = this.FindMatch(startInOld, endInOld, startInNew, endInNew);

            if (match != null)
            {
                if (startInOld < match.StartInOld && startInNew < match.StartInNew)
                {
                    this.FindMatchingBlocks(startInOld, match.StartInOld, startInNew, match.StartInNew, matchingBlocks);
                }

                matchingBlocks.Add(match);

                if (match.EndInOld < endInOld && match.EndInNew < endInNew)
                {
                    this.FindMatchingBlocks(match.EndInOld, endInOld, match.EndInNew, endInNew, matchingBlocks);
                }

            }
        }


        private Match FindMatch(int startInOld, int endInOld, int startInNew, int endInNew)
        {
            int bestMatchInOld = startInOld;
            int bestMatchInNew = startInNew;
            int bestMatchSize = 0;

            Dictionary<int, int> matchLengthAt = new Dictionary<int, int>();

            for (int indexInOld = startInOld; indexInOld < endInOld; indexInOld++)
            {
                var newMatchLengthAt = new Dictionary<int, int>();

                string index = this.oldWords[indexInOld];

                if (!this.wordIndices.ContainsKey(index))
                {
                    matchLengthAt = newMatchLengthAt;
                    continue;
                }

                foreach (var indexInNew in this.wordIndices[index])
                {
                    if (indexInNew < startInNew)
                    {
                        continue;
                    }

                    if (indexInNew >= endInNew)
                    {
                        break;
                    }


                    int newMatchLength = (matchLengthAt.ContainsKey(indexInNew - 1) ? matchLengthAt[indexInNew - 1] : 0) + 1;
                    newMatchLengthAt[indexInNew] = newMatchLength;

                    if (newMatchLength > bestMatchSize)
                    {
                        bestMatchInOld = indexInOld - newMatchLength + 1;
                        bestMatchInNew = indexInNew - newMatchLength + 1;
                        bestMatchSize = newMatchLength;
                    }
                }

                matchLengthAt = newMatchLengthAt;
            }

            return bestMatchSize != 0 ? new Match(bestMatchInOld, bestMatchInNew, bestMatchSize) : null;
        }

    }

    public class Match
    {
        public Match(int startInOld, int startInNew, int size)
        {
            this.StartInOld = startInOld;
            this.StartInNew = startInNew;
            this.Size = size;
        }

        public int StartInOld { get; set; }
        public int StartInNew { get; set; }
        public int Size { get; set; }

        public int EndInOld
        {
            get
            {
                return this.StartInOld + this.Size;
            }
        }

        public int EndInNew
        {
            get
            {
                return this.StartInNew + this.Size;
            }
        }

    }

    public class Operation
    {
        public Action Action { get; set; }
        public int StartInOld { get; set; }
        public int EndInOld { get; set; }
        public int StartInNew { get; set; }
        public int EndInNew { get; set; }

        public Operation(Action action, int startInOld, int endInOld, int startInNew, int endInNew)
        {
            this.Action = action;
            this.StartInOld = startInOld;
            this.EndInOld = endInOld;
            this.StartInNew = startInNew;
            this.EndInNew = endInNew;
        }
    }

    public enum Mode
    {
        character,
        tag,
        whitespace,
    }

    public enum Action
    {
        equal,
        delete,
        insert,
        none,
        replace
    }


}
