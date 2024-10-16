using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word
{
    public interface ICell
    {
        string Value { get; set; }

        void AlignRight();
    }
}
