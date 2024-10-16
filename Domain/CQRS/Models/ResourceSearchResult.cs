using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models
{
    public class ResourceSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string ResourceType { get; set; }

        public string JsonValues { get; set; }

        public Guid? ModuleId { get; set; }

        public Module Module { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ModelState State { get; set; } = ModelState.None;
    }
}
