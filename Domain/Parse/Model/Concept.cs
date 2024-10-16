

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Parse.Model
{
    public class Concept
    {
        public Guid UID { get; set; }
        public string Number { get; set; }
        public string Caption { get; set; }
        public string SortOrder { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        public string PageSource { get; set; }
        public string ConceptsWords { get; set; }
        public string TableName  { get; set; }
        public string XMLFile  { get; set; }

    }
}
