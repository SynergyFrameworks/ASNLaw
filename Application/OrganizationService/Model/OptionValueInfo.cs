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
    public class OptionValueInfo : IEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }
              
        [Required]
        [MaxLength(100)]
        public string DisplayText { get; set; }

        [Required]
        [MaxLength(50)]
        public string DataType { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Value { get; set; }

        [Required]       
        public Guid ActionOptionId { get; set; }



    }
}
