using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Domain.Group;
using Datalayer.Domain.Storage;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.CQRS.Models
{
    public class ProjectDocumentSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        public string Url { get; set; }

        public int Size { get; set; }

        public string Extension { get; set; }

        public bool IsOutput { get; set; }

        public Guid DocumentSourceId { get; set; }

        public DocumentSource DocumentSource { get; set; }


        //Audit
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ModelState State { get; set; } = ModelState.None;        

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? ModifiedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ModifiedBy { get; set; }


    }
}



