using Datalayer.Domain.Demographics;
using Datalayer.Domain;
using Datalayer.Domain.Group;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class PhoneProjection
    {
        public static Expression<Func<Phone, Phone>> PhoneInformation
        {
            get =>
                o => new Phone
                {
                    Id = o.Id,
                    CountryPrefix = o.CountryPrefix,
                    PhoneNumber = o.PhoneNumber,
                    PhoneType = o.PhoneType,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                    Clients = o.Clients.Select(client => new Datalayer.Domain.ASNClient {
                        Id = client.Id,
                        ClientNo = client.ClientNo,
                        Name = client.Name,
                        Description = client.Description,
                        CreatedBy = client.CreatedBy,
                        CreatedDate = client.CreatedDate
                    }).AsParallel().ToList(),
                    Organizations = o.Organizations.Select(org => new Datalayer.Domain.Organization { 
                        Id = org.Id,
                        Name = org.Name,
                        Description = org.Description,
                        CreatedBy = org.CreatedBy,
                        CreatedDate = org.CreatedDate,
                        SuperUserId = null
                    }).AsParallel().ToList()
                };
        }
    }
}
