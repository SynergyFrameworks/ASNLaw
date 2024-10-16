using Datalayer.Domain;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Models.MailBox;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class MailBoxSearchDetails
    {
        public static Expression<Func<MailBox, MailBoxSearchResult>> MailBoxSearch => o =>
                         new MailBoxSearchResult
                         {
                             Id = o.Id,
                             Server = o.Server,
                             FromAddress = o.FromAddress,
                             ServerUserName = o.ServerUserName,
                             ServerPassword = o.ServerPassword,
                             ConnectionSecurity = o.ConnectionSecurity,
                             CreatedBy = o.CreatedBy,
                             CreatedDate = o.CreatedDate,
                             ModifiedBy = o.ModifiedBy,
                             ModifiedDate = o.ModifiedDate,
                             MailBoxOwners = o.Organizations.Select(o => new MailBoxOwner { Id = o.Id, Name = o.Name, OwnerType = Enums.OwnerType.Organization })
                             .Union(o.Clients.Select(o => new MailBoxOwner { Id = o.Id, Name = o.Name, OwnerType = Enums.OwnerType.Client })).ToList(),
                         };
    }
}
