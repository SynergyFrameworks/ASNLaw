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
    public class ActionOptionInfo : IEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }

        [Required]
        public Guid ActionId { get; set; }

        [MaxLength(50)]
        public string Label { get; set; }
        public ICollection<ActionOption> ActionOptions { get; internal set; }

        public ICollection<OptionValue> OptionValues { get; internal set; }

    }
}
