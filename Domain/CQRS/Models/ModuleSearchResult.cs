using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;


namespace Infrastructure.CQRS.Models
{
    public class ModuleSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string Version { get; set; }

        public string VersionTag { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LicenseUrl { get; set; }

        public string ProjectUrl { get; set; }

        public bool RequireLicenseAcceptance { get; set; }

        public string Notes { get; set; }

        public string Tags { get; set; }

        public bool IsRemovable { get; set; }

        public bool IsInstalled { get; set; }

        public string Authors { get; set; }

        public string Owners { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ModelState State { get; set; } = ModelState.None;

        public ICollection<ASNGroup> Groups { get; set; }

        //public ICollection<Subscription> Subscriptions { get; set; };
        public ICollection<Resource> Resources { get; set; }
    }
}
