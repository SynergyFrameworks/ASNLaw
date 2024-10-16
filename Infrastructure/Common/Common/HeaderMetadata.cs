using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    class HeaderMetadata
    {
        public string DataKey { get; set; }
        public string DisplayValue { get; set; }
        public string Type { get; set; }
        public bool IsVisible { get; set; }
        public bool IsRange { get; set; }
    }
}
