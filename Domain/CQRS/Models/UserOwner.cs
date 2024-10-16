using Infrastructure.CQRS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models
{
    public class UserOwner
    {
        public Guid Id { get; set; }

        [NotMapped]
        public OwnerType OwnerType { get; set; }

        public string Name { get; set; }
    }
}
