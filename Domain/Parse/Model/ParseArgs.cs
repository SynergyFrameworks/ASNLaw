using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Domain.Settings.Model;

namespace Domain.Parse.Model
{
    public class ParseArgs : EventArgs, IParseArgs
    {

        public ParseArgs()
        {
        }

        public Guid ID { get; set; }
        public ParseTypeDef.ParseType ParseType { get; set; }
        public IFormFile File { get; set; }
        public Guid TaskID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId  { get; set; }
        public string txtContent { get; set; }
        public string[] txtContentLinesArray { get; set; }
        public int MaxParamLen { get; set; } = 12;
        public IList<ParseParameter> Parameters { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        public ICollection<Concept> Concepts { get; set; }
        public ICollection<DictionaryContext> DictionaryTerms { get; set; }
    }
}
