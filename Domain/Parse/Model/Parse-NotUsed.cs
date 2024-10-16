using System;
using System.Collections.Generic;

namespace Domain.Parse.Model
{
  public class ParseNotUsed 
    {
        public ParseNotUsed(
            Guid taskID,
            Guid projectID,
            Guid userId,
            ICollection<ParseSegmentResult> parseSectionResults,
            ICollection<ParameterFound> parametersFound,
            ICollection<KeywordFound> keywordFound) {
            
            TaskID = taskID;
            ProjectID = projectID;
            UserId = userId;
            ParametersFound = parametersFound;
            ParseResults = parseSectionResults;
            KeywordsFound = keywordFound;       // new HashSet<KeywordFound>();
        }

        public Guid TransactionID { get; init; }
        public Guid TaskID { get; init; }
        public Guid ProjectID { get; init; }
        public Guid UserId { get; init; }
        public string[] Libraries { get; set; }


        public ICollection<ParseSegmentResult> ParseResults { get; set; }
        public ICollection<ParameterFound> ParametersFound { get; set; }
        public ICollection<KeywordFound> KeywordsFound { get; set; }


    }
}
