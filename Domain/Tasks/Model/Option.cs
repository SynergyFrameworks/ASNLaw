using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class Option
    {
        public virtual IList<OptionValue> Values { get; set; }
        public virtual String Name { get; set; }
        public virtual String Display { get; set; }

        public string DataType
        {
            get => default;
            set
            {
            }
        }
    }

}
