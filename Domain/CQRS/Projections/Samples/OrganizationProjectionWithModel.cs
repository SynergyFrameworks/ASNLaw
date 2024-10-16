using Datalayer.Domain.Demographics;
using Datalayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class OrganizationDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string WebUrl { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
    public static class OrganizationProjectionWithModel
    {
        public static Expression<Func<Organization, OrganizationDetails>> OrganizationInformation
        {
            get =>
                o => new OrganizationDetails
                {
                    Id = o.Id,
                    Name = o.Name,
                    ImageUrl = o.ImageUrl,
                    WebUrl = o.WebUrl,
                    Addresses = o.Addresses,
                    Phones = o.Phones, 
                    



                };
        }
    }
}
