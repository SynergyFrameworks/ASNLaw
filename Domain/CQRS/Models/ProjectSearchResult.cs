using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;

namespace Infrastructure.CQRS.Models
{
    public class ProjectSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        //public ICollection<ProjectDocument> ProjectDocuments { get; set; }

        //public ICollection<Series> Series { get; set; }

        //public ICollection<Task> Tasks { get; set; }

        public ModelState State { get; set; } = ModelState.None;

        public Guid GroupId { get; set; }

        public string ImageUrl { get; set; }

        //public Group Group { get; set; }
    }
}



