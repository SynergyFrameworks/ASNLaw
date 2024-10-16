using Datalayer.Contracts;
using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrganizationService.Model
{
    public class ActionInfo : IEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        
        [MaxLength(1000)]
        public string Description { get; set; }

        public ICollection<ActionOption> ActionOptions { get; internal set; }
    }
}
