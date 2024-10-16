using Datalayer.Domain;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class ClientProjection
    {
        public static Expression<Func<ASNClient, ASNClient>> ClientInformation
        {
            get =>
                o => new ASNClient
                {
                    Id = o.Id,
                    ClientNo = o.ClientNo,
                    Description = o.Description,
                    Name = o.Name,
                    ImageUrl = o.ImageUrl,
                    WebUrl = o.WebUrl,
                    Addresses = o.Addresses,
                    Phones = o.Phones,
                    OrganizationId = o.OrganizationId
                };
        }
    }
}
