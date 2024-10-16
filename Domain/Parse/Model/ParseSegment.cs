using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Parse.Model;

namespace Domain.Parse.Model
{
    public class ParseSegment
    {

       public int ID { get; set; }
       public string Segment { get; set; }
       public int SegmentLength { get; set; }

    }
}
