using Domain.Parse.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domain.Parse.Utility
{
    static class StringUtility
    {

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string Valid_ASCII(string strString)
        {
            int x = 0;


            for (x = 1; x < strString.Length; x++)
            {
                if ((int)Convert.ToChar(strString.Substring(x, 1)) > 126 || (int)Convert.ToChar(strString.Substring(x, 1)) < 32)  // VB code: if (String.Asc(Strings.Mid(strString, x, 1)) > 126 | Strings.Asc(Strings.Mid(strString, x, 1)) < 32)              
                {
                    strString.Replace(strString.Substring(x, 1), ""); // VB code: strString = ReplaceChars(strString, Strings.Mid(strString, x, 1), " ");                                    
                }
            }

            return strString;
        }

        private static string _dots = " ...";
        private static int _dotsLen = _dots.Length;

        public static string TruncateString(
            string strInput, int startIdx, int maxLen)
        {
            int len = strInput.Length;
            int x = startIdx;
            while (x < len && strInput[x] == ' ') ++x; //...skip leading blanks

            int extract_len = len - x;

            if (extract_len <= maxLen) //...no need to adjust
            {
                if (x == 0) //...extract everything from the start, i.e. just return the string as-is
                    return strInput;

                return strInput.Substring(x);
            }

            // At this point, we must adjust the length
            maxLen -= _dotsLen; //...make room for ellipsis

            int end_idx = x + maxLen; //...is < len!

            while (end_idx > x && strInput[end_idx] != ' ') --end_idx;
            while (end_idx > x && strInput[end_idx - 1] == ' ') --end_idx;
            extract_len = end_idx - x;
            if (extract_len <= 0) return _dots;
            return strInput.Substring(x, extract_len) + _dots;
        }
       
        public static string Truncate_String(string strInput, int intMax_Length, bool booEnd_With_Dots = false)
        {
            int i;
            int intUBound;
            int intAdjMax_Length;
            string[] strString;
            string strNew_String;

            strNew_String = string.Empty; // >> Set Default 

            try
            {
                bool booAdjEnd_With_Dots = false; // Added 09.21.2013

                // >> Split on Carriage Returns
                int nl_idx = strInput.IndexOf('\n');
                if (nl_idx >= 0) //...this is never true considering where this routine is called from!
                {
                    if (nl_idx > 0 && strInput[nl_idx - 1] == '\r') --nl_idx;

                    strNew_String = strInput.Substring(0, nl_idx);
                }
                else
                    strNew_String = strInput;

                if (strNew_String.Length <= intMax_Length)
                {
                    return strNew_String; //...no modifications made, making me think the cleanup code is not necessary (or should be applied here also)
                }
                else
                {
                    strInput = strNew_String;
                    booAdjEnd_With_Dots = true;
                }

                if (booEnd_With_Dots && (intMax_Length < strInput.Length))
                    intAdjMax_Length = intMax_Length - _dotsLen;
                else
                    intAdjMax_Length = intMax_Length;

                strString = Strings.Trim(strInput).Split(' ');
                intUBound = strString.GetUpperBound(0);
                strNew_String = string.Empty; // Reset Defualt >> Added 09.21.2013
                for (i = 0; i <= intUBound; i++)
                {
                    if (Strings.Len(strNew_String + " " + strString[i]) <= intAdjMax_Length)
                        strNew_String = strNew_String + " " + strString[i];
                    else
                        break;
                }

                if (booAdjEnd_With_Dots)
                    strNew_String = strNew_String + _dots;

                // >> Cleanup Title
                strNew_String = Clean_String(strNew_String);

                //            strNew_String = ReplaceChars( strNew_String, "'", " " ); // >> Remove single quotes
                //            strNew_String = ReplaceChars( strNew_String, ((char)34).ToString(), " " ); // >> Remove double quotes
                //            Valid_ASCII( strNew_String );
                //            strNew_String = Strings.Trim( strNew_String );

                return strNew_String;
            }
            catch (Exception ex)
            {
                throw ex; //MsgBox( Information.Err.Description + Constants.vbCrLf + Constants.vbCrLf + "An error has occurred while truncating captions that are too long.", MsgBoxStyle.Exclamation, "Error No.: " + Information.Err.Number );
            }
        }


        public static string GetCaption(string strInput, int intMax_Length, bool booEnd_With_Dots)
        {
            string functionReturnValue = null;
            const string strDOTS = " ...";
            int i = 0;
            int intUBound = 0;
            int intAdjMax_Length = 0;
            string[] strString = null;
            string strNew_String = null;

            strNew_String = string.Empty;
            //>> Set Default 

            // ERROR: Not supported in C#: OnErrorStatement
            bool booAdjEnd_With_Dots = false;
            booAdjEnd_With_Dots = false;

            //>> Split on Carriage Returns
            if (strInput.Contains(System.Environment.NewLine))
            {
                string[] strNewSegment = null;
                strNewSegment = strInput.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                strNew_String = strNewSegment[0];

                if (strNew_String.Length <= intMax_Length)
                {
                    functionReturnValue = strNew_String;
                    return functionReturnValue;
                }
                else
                {
                    strInput = strNew_String;
                    booAdjEnd_With_Dots = true;
                }
            }

            if (booEnd_With_Dots)
            {

                if (intMax_Length < strInput.Length)
                {
                    intAdjMax_Length = intMax_Length - strDOTS.Length;
                }
                else
                {
                    intAdjMax_Length = intMax_Length;
                }
            }
            else
            {
                intAdjMax_Length = intMax_Length;
            }
            strString = strInput.Split(' ');
            intUBound = strString.Length;
            strNew_String = string.Empty;

            for (i = 0; i < intUBound; i++)
            {
                if ((strNew_String + " " + strString[i]).Length <= intAdjMax_Length)
                {
                    strNew_String = strNew_String + " " + strString[i];
                }
                else
                {
                    break;
                }
            }

            if (booAdjEnd_With_Dots)
            {
                strNew_String = strNew_String + strDOTS;
            }

            //>> Cleanup Title
            strNew_String = strNew_String.Replace("'", " ");
            //>> Remove single quotes
            strNew_String = strNew_String.Replace((char)34, ' ');
            //>> Remove double quotes
            Valid_ASCII(strNew_String);
            strNew_String = strNew_String.Trim();

            return strNew_String;

        }

        public static string Clean_String(string str)
        {
            StringBuilder sb = new StringBuilder();

            int len = str.Length, i = 0;

            while (i < len && char.IsWhiteSpace(str[i])) ++i;

            for (; i < len; ++i)
            {
                char ch = str[i];
                if (((int)ch < 32) || ((int)ch > 126) || (ch == '\'') || (ch == '"'))
                {
                    sb.Append(' ');
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().TrimEnd();
        }

        //public static void Remove_Prefix_Tabs(ref string sLine) //...Caution: Will make changes to the "immutable" string 'sLine'! The parameter does not have to be 'ref' but I made it such to make clear that the string would change.
        //{
        //    int len = sLine.Length;
        //    unsafe
        //    {
        //        fixed (char* curr = sLine)
        //        {
        //            for (int i = 0; i < len; ++i)
        //            {
        //                if (curr[i] == '\t') curr[i] = ' ';
        //            }
        //        }

        //    }
        //}

        public static int FindParameterLevel(string sValue, IList<ParameterLevel> parameterLevels)
        {
            int nRow = 0;

            foreach (ParameterLevel param in parameterLevels)
            {
                if (param.Parameter == sValue)
                    return nRow;

                nRow = nRow + 1;
            }

            return -1; // >> Not Found
        }

        /// <summary>
        /// GetLineNumber
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lineToFind"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static int GetLineNumber(string text, string lineToFind, StringComparison comparison = StringComparison.CurrentCulture)
        {
            int lineNum = 0;
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNum++;
                    if (line.Equals(lineToFind, comparison))
                        return lineNum;
                }
            }
            return -1;
        }

        //private static string AddSlash(string str)
        //{
        //    return (str.Length > 0 && str[str.Length - 1] == '\\') ? str : str + "\\";
        //}

        public static int countstring(string s)
        {
            int count = 1;
            int start = 0;
            while ((start = s.IndexOf('\n', start)) != -1)
            {
                count++;
                start++;
            }
            return count;
        }

        public static int CountLines(string str)
        {
            int lines = 1;
            int index = 0;
            while (true)
            {
                index = str.IndexOf('\n', index);
                if (index < 0)
                    break;
                lines++;
                index++;
            }
            return lines;
        }

        /// <summary>
        /// TODO the universal GetKeywords4PSection
        /// </summary>
        /// <param name="IndexStart"></param>
        /// <param name="IndexEnd"></param>
        /// <param name="ParseSection_UID"></param>
        /// <param name="keywordsfound"></param>
        /// <returns></returns>
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///TODO WRONG GetKeywords4PSection!! this method should be universal for boths parsers... This NEEDS TO BE CHECKED AND VERIFIED
        ///Wasn't sure whether should iterate of Parse Section
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static IList<KeywordFound> GetKeywords4PSection(int IndexStart, int IndexEnd, string ParseSection_UID, IList<ParseSegment> parseSection, IList<KeywordFound> keywordsfound)
        {
            int intRowCount;

            if (keywordsfound == null)
                return null;

            intRowCount = parseSection.Count;

            if (keywordsfound.Count == 0 | intRowCount == 0)
                return null;

            int keywordIndex;
            StringBuilder sb = new StringBuilder();
            Keyword sKeyword;
            int foundCount = 0;
            string sFoundInPSections;

            intRowCount = intRowCount - 1;
            IList<KeywordFound> KeywordsFoundRst = new List<KeywordFound>();
            foreach (KeywordFound keywordFound in keywordsfound)
            //  for (i = 0; i <= intRowCount; i++)
            {
                keywordIndex = (int)keywordFound.Index;

                if (keywordIndex >= IndexStart & keywordIndex <= IndexEnd)
                {
                    sKeyword = keywordFound.Keyword;
                    foundCount = foundCount + 1;
                    /////////////////////////////////////////////////////////////////////////////
                    // TODO Convert to return object  
                    // sb = AppendKeywordsSB(sb, sKeyword);

                    /////////////////////////////////////////////////////////////////////
                    // If foundCount > 1 Then
                    // sb.Append(", ") '>> Place a comma after the previous keyword found for this parsed section
                    // End If
                    // sb.Append(sKeyword) '>> this will be returned

                    // >> Save Parsed Section UID as a comma delimited value into the Keywords Found dataset
                    if (null == keywordFound.FoundInPSections)
                        sFoundInPSections = string.Empty;
                    else
                        sFoundInPSections = (string)keywordFound.FoundInPSections;

                    if (Strings.Trim(sFoundInPSections) == string.Empty)
                        keywordFound.FoundInPSections = ParseSection_UID;
                    else
                        keywordFound.FoundInPSections = string.Concat(sFoundInPSections, ",", ParseSection_UID);
                }

                KeywordsFoundRst.Add(keywordFound);

            }

            return KeywordsFoundRst;
        }

        ////TODO question 
        //public static StringBuilder AppendKeywordsSB(StringBuilder sb, string Keyword)
        //{
        //    string sContent;
        //    int intLoc;

        //    if (sb.Length == 0)
        //    {
        //        sb.Append(Keyword);
        //        sb.Append(" ");
        //        sb.Append("[1]");
        //        return sb;
        //    }

        //    sContent = sb.ToString();

        //    intLoc = sContent.IndexOf(Keyword, 0);
        //    if (intLoc < 0)
        //    {
        //        sb.Append(", ");
        //        sb.Append(Keyword);
        //        sb.Append(" ");
        //        sb.Append("[1]");
        //        return sb;
        //    }


        //    string sLeft;
        //    int intLoc2;
        //    string sMid;
        //    string sRight;
        //    int intNumber;
        //    int intLoc3;

        //    sLeft = sContent.Substring(0, intLoc + Keyword.Length + 1);
        //    intLoc2 = sContent.IndexOf("]", intLoc);
        //    sRight = sContent.Substring(intLoc2 + 1);
        //    intLoc3 = sContent.IndexOf("[", intLoc);

        //    sMid = Strings.Trim(sContent.Substring(intLoc3 + 1, intLoc2 - intLoc3 - 1));
        //    // sMid = sMid.Substring(1, sMid.Length - 1)
        //    if (Strings.IsNumeric(sMid) == true)
        //    {
        //        intNumber = System.Convert.ToInt32(sMid);
        //        intNumber = intNumber + 1; // >> Increment Qty 
        //        sMid = " [" + intNumber.ToString() + "]"; // >> Reformat
        //        sb.Clear();
        //        // >> Rebuild
        //        sb.Append(sLeft);
        //        sb.Append(sMid);
        //        sb.Append(sRight);
        //        return sb;
        //    }

        //    return sb; // >> Should never occur
        //}

        public static int GetLineEnd(string[] textContent, int currentRow, int rowCount)
        {
 
            int endRow = 0;

            if (currentRow == rowCount)
                endRow = textContent.Length; 
             // endRow = Convert.ToInt32(parametersFound[currentRow + 1].LineStart) - 1;

            return endRow;
        }

        public static int GetIndexEnd(string[] textContent, int currentRow, int rowCount, ref int startIndex, int text_length)
        {
            int endIndex;

            endIndex = 0;

            if (currentRow == rowCount)
                endIndex = text_length;
            ///else

            //
            //TODO invetigate to see if needed  endIndex = System.Convert.ToInt32(parametersFound[currentRow + 1].Index) - 1;


            if (endIndex == startIndex)
                endIndex = startIndex + textContent[currentRow].Length;


            return endIndex;
        }





    }
}
