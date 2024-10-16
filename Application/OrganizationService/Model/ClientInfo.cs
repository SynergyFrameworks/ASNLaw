using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationService.Model
{
    public class ClientInfo : IEntity
    {
        public Guid Id { get; set; }

        [MaxLength(20)]
        public string ClientNo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string WebUrl { get; set; }

        [Required]
        public Guid OrganizationId { get; set; }
    }
}
