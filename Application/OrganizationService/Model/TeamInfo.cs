using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationService.Model
{
    public class TeamInfo: IEntity
    {
        public Guid Id { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        public ClientInfo Client { get; set; }

     
    }
}
