using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrganizationService.Model
{
    public class ProjectDocumentInfo : IEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int Size { get; set; }

        public string Extension { get; set; }

        public bool IsOutput { get; set; }

        public string Url { get; set; }

        [Required]
        public Guid DocumentSourceId { get; set; }





    }
}
