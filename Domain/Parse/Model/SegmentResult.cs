using System;
using System.Collections.Generic;


namespace Domain.Parse.Model
{
    public class SegmentResult
    {

        public Guid UID { get; set; }
        public int Page { get; set; }  // Not set up but would be something to work for might even need a type definition 
        public string Parameter { get; set; }
        public int Number { get; set; }
        public Guid Parent { get; set; }
        public int Rows { get; set; }
        public int LineStart { get; set; }
        public int LineEnd { get; set; }
        public int ColumnStart { get; set; }
        public int ColumnEnd { get; set; }
        public int IndexStart { get; set; }
        public int IndexEnd { get; set; } 
        public string Caption { get; set; }
        public int SortOrder { get; set; }
        public ICollection<ParameterFound> ParameterFound { get; set; }
        public ICollection<KeywordFound> KeywordFound { get; set; }
        public ParseSegment Segment { get; set; }
       


    }
}
