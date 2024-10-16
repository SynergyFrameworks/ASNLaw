using Datalayer.Contracts;
using Datalayer.Domain.Security;
using System;

namespace Infrastructure.CQRS.Models
{
    public class OrganizationOuput: IEntity
    {

        public Guid Id { get; set; }

        public string Name { get; set; }


        public string Description { get; set; }


        public string CompanyName { get; set; }


        public string WebUrl { get; set; }


        public string ImageUrl { get; set; }

        public User User { get; set; }


    }
}
