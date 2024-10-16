using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scion.Business.Security
{
    class Options<T> where T: class
    {
        public virtual String OptionsId { get; set; }
        public virtual ICollection<T> Values { get; set; }
        public virtual String Name { get; set; }
        public virtual String Display { get; set; }

        public Guid ActionId
        {
            get => default;
            set
            {
            }
        }

        public string DataType
        {
            get => default;
            set
            {
            }
        }
    }
}
