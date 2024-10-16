using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Parse.Model
{
    public class ValidationResult
    {

        public Guid ResultsUID { get; set; }
        public int Number { get; set; }
        public string Caption { get; set; }
        public int Severity { get; set; }
        public string Description { get; set; }
      


    }
}
