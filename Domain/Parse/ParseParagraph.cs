using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Domain.Parse.Model;
using Domain.Parse.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Parse
{

    [SimpleJob(RunStrategy.Monitoring, targetCount: 10, id: "ParsingJob")]
    [MinColumn, Q1Column, Q3Column, MaxColumn]
    
    
    public class ParseParagraph : IParse
    {

        public ICollection<ParseSegmentResult> ParseSegmentResults { get; set; } = default(ICollection<ParseSegmentResult>);
        public IList<Keyword> Keywords { get; set; }
        private ICollection<KeywordFound> KeywordsFound { get; set; }


        public async Task<ICollection<ParseSegmentResult>> Parse(ParseArgs parseArgs, IProgress<double> progress, CancellationToken cancellationToken = default(CancellationToken)) 
        {
        
                  if (parseArgs.txtContent == string.Empty)
                     {
                      throw new ArgumentNullException(nameof(parseArgs.txtContent));
            
                     }

            int Counter = 0;
       
            ParseSegmentResults = new Collection<ParseSegmentResult>();
            Keywords = new Collection<Keyword>();

            char[] delimiters = new char[] { '\r', '\n' }; 
            string[] lines = parseArgs.txtContent.Split(delimiters, StringSplitOptions.RemoveEmptyEntries); // Split document text into paragraphs
            
          foreach (string line in lines)
          {
                 
             var modLine = StringUtility.RemoveSpecialCharacters(line.Trim());
             
                if (modLine != string.Empty)
                {

                    var IndexStart = 0;
                    var IndexEnd = line.Length;

                   var keywords = GetKeywords4PSection(IndexStart, IndexEnd, line, parseArgs.Keywords);



                    var keyword = new Keyword
                    {
                        Word = string.Empty,
                        Category = string.Empty
                    };

                    //////////////////////////////////////////////////////////////////////////////////////////////
                    /////TODO FindKeywords method with return of collection of Keywords Method we have somewhere
                    ///////////////////////////////////////////////////////////////////////////////////////////

                    //// if (keyword.Word != "" || keyword.Word != string.Empty)

                    ParseSegment parseSegment = new ParseSegment
                    {
                        ID = Counter,
                        Segment = line,
                        SegmentLength = line.Length
                       
                    };

                    ParseSegmentResult parseSectionResult = new ParseSegmentResult
                    {
               
                        UID = Guid.NewGuid(),
                        SortOrder = Counter,
                        NumberOfLines = Counter,              
                        Caption = StringUtility.GetCaption(line, 50, true),
                        Parent = parseArgs.ID,  // TODO This needs to be set correctly
 
                        IndexStart = Counter, ///TODO
                        IndexEnd = Counter,///TODO

                        Segment = parseSegment,

                     
                    };
                   
                    ParseSegmentResults.Add(parseSectionResult);

                }
               
                Counter++;                            
          }

            return ParseSegmentResults;

         }

     

        private ICollection<KeywordFound> GetKeywords4PSection(int IndexStart, int IndexEnd, string ParseSection_UID, ICollection<Keyword> Keywords)
        {
            int intRowCount;

            if (KeywordsFound == null)
                return null;

            intRowCount = KeywordsFound.Count;

            if (KeywordsFound.Count == 0 | intRowCount == 0)
                return null;

          
            int keywordIndex;
        
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

                KeywordsFound.Add(keywordFound);  //TODO  COME ON MAN

            }

            return KeywordsFound;
        }




    }
}
