using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Domain.Parse.Model;
using Domain.Parse.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Parse.Contracts;
using MongoDB.Bson;

namespace Domain.Parse
{
    [SimpleJob(RunStrategy.Monitoring, targetCount: 10, id: "ParsingJob")]
    [MinColumn, Q1Column, Q3Column, MaxColumn]
    public class ParseLegal : IParse, IDocument
    {

        public Object ParseId { get; set; }
        public DateTime DateTimeUTC { get; set; }


        private IList<ParameterLevel> parameterLevels = new List<ParameterLevel>();
        private static readonly char[] _wordEndChars = new char[] { ' ', '\'', '"' }; //, '\t' }; //...add the tab back in if we remove the call to 'Remove_Prefix_Tabs'


        // Func<string>(T arg) Parameter = delegate(IList<>)

        // >>> Line Number Documents <<<
        // Steps:
        // 1.	Build cross-reference table, containing Row ID to Document Row Number
        // 2.	Remove Row IDs
        // 3.	Parse into segments
        // 4.	Re-insert Row IDs into parsed segments

        private IList<ParseSegmentResult> ParseSegmentResults { get; set; } = new List<ParseSegmentResult>();
        private List<ParameterFound> ParametersFound { get; set; } = new List<ParameterFound>();
        private IList<ValidationResult> ValidationResults { get; set; } = new List<ValidationResult>();
        private IList<KeywordFound> KeywordsFound { get; set; } = new List<KeywordFound>();
        ObjectId IDocument.ParseId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Benchmark]
        public async Task<ICollection<ParseSegmentResult>> Parse(ParseArgs parseLegal, IProgress<double> progress, CancellationToken cancellationToken = default(CancellationToken))
        {
            /// Pass 1 Deterine where are the locations of all the parameters
            IList<ParameterFound> ParameterFound = Analyze_Doc_Parameters(parseLegal, progress);

            int text_length = parseLegal.txtContent.Length;
            string[] textContentLineArr;
            int lineCount = 0;

            textContentLineArr = Regex.Split(parseLegal.txtContent, "\r\n|\r|\n");
            lineCount = textContentLineArr.Length;

            Guid pID = parseLegal.ID;

            /// Pass 2 Map Out where all the parseSegmentResults
            IList<ParseSegmentResult> parseSegmentResults = await Task.FromResult(Parse_SegmentResults(parseLegal.txtContent, textContentLineArr, text_length, pID, ParameterFound)).ConfigureAwait(false);

            if (parseSegmentResults.Count() == 0)
                return null;
            //count = count - 1;
            //int numComplete = 0;


            //double percentParse = 1;
            //percentParse /= (double)parseSegmentResults.Count;
            //double startingPercent = numComplete * percentParse * 100;
            /////////////////////////////////////////////////////////////

            //numComplete++;
            //double percent = numComplete;
            //percent /= parseSegmentResults.Count;
            //progress.Report(percent * 100);
            //////////////////////////////////////
            //progress.Report(100);




            //foreach (ParseSegmentResult parseSegmentResult in parseSegmentResults)

            //{
            //    double percentParse = 1;
            //    percentParse /= (double)parseSegmentResults.Count;
            //    double startingPercent = numComplete * percentParse * 100;

            //    indexStart = Convert.ToInt32(parseSegmentResult.IndexStart);
            //    indexEnd = Convert.ToInt32(parseSegmentResult.IndexEnd);


            //    if (indexStart < 0) //...this is an error!
            //    {


            //        ///////////////TODO Validation Method

            //        //ValidationResult ValidationResult = new ValidationResult()
            //        //{

            //        //    ResultsUID = parseSectionResult.UID,
            //        //    Number = parseSectionResult.Number,
            //        //    Caption = parseSectionResult.Caption,
            //        //    Severity = 1,
            //        //    Description = "Start Index Error – Start Index = " + indexEnd.ToString(),
            //        //};
            //        //ValidationResults.Add(ValidationResult);


            //    }

            //    else

            //    {


            //        //ParseSegment psection = parseSegmentResult.Segment;
            //        //Delta = indexEnd - indexStart; // >> This seems to work, need to continue to test
            //        //ParseSegment Section = new ParseSegment()
            //        //{

            //        //    ID = parseSegmentResult.Number,
            //        //    //LineNumbers = parseResult.Rows,
            //        //    Segment = psection?.Segment
            //        //    .Substring(indexStart, Delta)

            //        //};

            //        //ParseSegmentList.Add(Section);

            //        numComplete++;
            //        double percent = numComplete;
            //        percent /= parseSegmentResults.Count;
            //        progress.Report(percent * 100);


            //    }

            //    progress.Report(100);
            //}

            return ParseSegmentResults;
        }

        /// <summary>
        /// Analyze_Doc_Parameters
        /// </summary>
        /// <param name="textContent"></param>
        /// <param name="booParse_Hierarchical"></param>
        /// <param name="strMsg"></param>
        /// <param name="intMax_Param_Len"></param>
        /// <param name="progress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Benchmark]
        private IList<ParameterFound> Analyze_Doc_Parameters(ParseArgs parseLegalContext, IProgress<double> progress)
        {

            int i;
            int nCharIndex;
            int currentLineLength;
            string strLast_Parameter = string.Empty;
            string[] ContentLineArray = Regex.Split(parseLegalContext.txtContent, "\r\n|\r|\n");
            int lineCount = ContentLineArray.Length;
            int patternCount = parseLegalContext.Parameters.Count;
            int currentLineStartIndex = 0;

            for (i = 0; i < lineCount; i++)
            {
                nCharIndex = currentLineStartIndex;
                string currentLine = Strings.Trim(ContentLineArray[i]);
                currentLineLength = currentLine.Length;
                currentLine = currentLine.Replace("\t", " ");

                if (currentLineLength > 1)
                {
                    int endIndex = currentLine.IndexOfAny(_wordEndChars);
                    string firstWord = (endIndex >= 0) ? currentLine.Substring(0, endIndex) : null;

                    if (!string.IsNullOrEmpty(firstWord))
                    {
                        PatternList patterns = new(parseLegalContext.Parameters.Select(o => o.Parameter).ToArray());

                        for (int p = 0; p < patternCount; p++)
                        {
                            PatternList.PatternDef pattern = patterns[p];
                            if (pattern.Matches(firstWord))
                            {
                                var parameterFound = new ParameterFound()
                                {
                                    Parameter = pattern.Pattern,
                                    Index = nCharIndex,
                                    StartLine = i,
                                    Found = firstWord,
                                    Caption = currentLine.Length > 50 ? currentLine.Substring(0, 50) : currentLine,
                                    ParameterLength = firstWord.Length,
                                    //Parent = null
                                };


                                if (ParametersFound.Count > 0)
                                    parameterFound.Parent = ParametersFound
                                                            .TakeWhile(x => x.Parameter != parameterFound.Parameter)
                                                            .DefaultIfEmpty(parameterFound)
                                                            .LastOrDefault();

                                ParametersFound.Add(parameterFound);
                            }
                        }
                    }


                }
                currentLineStartIndex += currentLine.Length + 1;
            }
            return ParametersFound;
        }

        [Benchmark]
        private IList<ParseSegmentResult> Parse_SegmentResults(string docText, string[] docTextArray, int text_length, Guid pID, IList<ParameterFound> parametersFound)
        {


            if (docTextArray.Length < 1)
                return null;

            int i;
            int intRowCount;

            intRowCount = parametersFound.Count;
            if (intRowCount == 0)
                return null;
            intRowCount = intRowCount - 1; // >> Because Rows are zero based


            int lineEnd;
            i = 0;

            foreach (ParameterFound Parameter in ParametersFound)
            {

                lineEnd = StringUtility.GetLineEnd(docTextArray, i, intRowCount);

                var Delta = (Parameter.Index + Parameter.ParameterLength) - Parameter.Index;

                ParseSegment Segment = new ParseSegment()
                {

                    ID = i,
                    //LineNumbers = parseResult.Rows,
                    Segment = docText
                     .Substring(Parameter.Index, docTextArray[i].Length),
                    SegmentLength = docText
                    .Substring(Parameter.Index, docTextArray[i].Length).Length

                };

                ParseSegmentResult ParseSectionResult = new ParseSegmentResult()
                {

                    Caption = Parameter.Caption,

                    IndexEnd = Parameter.Index + Parameter.Found.Length,
                    IndexStart = Parameter.Index,
                    KeywordsFound = GetKeywords4PSection(Parameter.Index, (Parameter.Index + Parameter.Found.Length), Parameter.Index.ToString()),

                    //LineEnd = Parameter.SegmentLength,
                    //LineStart = Parameter.LineNumber,


                    NumberOfLines = docTextArray.Length,
                    Parent = pID,
                    UID = Guid.NewGuid(),
                    SortOrder = i++,
                    Segment = Segment,
                    ParameterFound = Parameter



                };


                ParseSegmentResults.Add(ParseSectionResult);


            }


            return ParseSegmentResults;
        }




        /// <summary>
        ///     ''' Get Keywords found in parsed section per previous keyword search
        ///     ''' </summary>
        ///     ''' <param name="IndexStart"></param>
        ///     ''' <param name="IndexEnd"></param>
        ///     '''<param name="ParseSection_UID"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        private IList<KeywordFound> GetKeywords4PSection(int IndexStart, int IndexEnd, string ParseSection_UID)
        {
            int intRowCount;

            if (KeywordsFound == null)
                return null;

            intRowCount = KeywordsFound.Count;

            if (KeywordsFound.Count == 0 | intRowCount == 0)
                return null;
            int keywordIndex;
            //StringBuilder sb = new StringBuilder();
            Keyword sKeyword;
            int foundCount;
            string sFoundInPSections;

            foundCount = 0;

            intRowCount = intRowCount - 1;

            foreach (KeywordFound keywordFound in KeywordsFound)

            {
                keywordIndex = (int)keywordFound.Index;

                if (keywordIndex >= IndexStart & keywordIndex <= IndexEnd)
                {
                    sKeyword = keywordFound.Keyword;
                    foundCount = foundCount + 1;

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

                KeywordsFound.Add(keywordFound);

            }

            return KeywordsFound;
        }




    }
}
