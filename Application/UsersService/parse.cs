using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService
{
    public class parse
    {

        public class Rootobject
        {
            public string id { get; set; }
            public Element element { get; set; }
        }

        public class Element
        {
            public string name { get; set; }
            public string IsDataSet { get; set; }
            public string UseCurrentLocale { get; set; }
            public Complextype complexType { get; set; }
        }

        public class Complextype
        {
            public Choice choice { get; set; }
        }

        public class Choice
        {
            public string minOccurs { get; set; }
            public string maxOccurs { get; set; }
            public Element1 element { get; set; }
        }

        public class Element1
        {
            public string name { get; set; }
            public Complextype1 complexType { get; set; }
        }

        public class Complextype1
        {
            public Sequence sequence { get; set; }
        }

        public class Sequence
        {
            public Element2[] element { get; set; }
        }

        public class Element2
        {
            public string name { get; set; }
            public string type { get; set; }
            public string minOccurs { get; set; }
        }



    }
}
