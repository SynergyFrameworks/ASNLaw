using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scion.Infrastructure.Common;

namespace Scion.Data.Common.Model
{
    public class ParseParameter : AuditableEntity
    {
        public ParseParameter()
        {
        }
        [Key]
        public Guid Id { get; set; }
        public string Parameter { get; set; }

    }
}
