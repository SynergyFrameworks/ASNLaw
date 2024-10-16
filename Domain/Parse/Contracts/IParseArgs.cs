using System;
using System.Collections.Generic;

namespace Domain.Parse.Model
{
    public interface IParseArgs
    {
  
        int MaxParamLen { get; set; }
        IList<ParseParameter> Parameters { get; set; }
        ICollection<Keyword> Keywords { get; set; }
        Guid ProjectID { get; set; }
        Guid TaskID { get; set; }
        string txtContent { get; set; }
        string[] txtContentLinesArray { get; set; }
        Guid UserId { get; set; }
    }
}