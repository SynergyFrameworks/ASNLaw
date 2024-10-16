using System;

namespace Domain.Parse.Model
{
    public class KeywordFound
    {

        public Keyword Keyword { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public int Index { get; set; }
        public string  FoundInPSections { get; set; }

    }
}
