using Datalayer.Domain;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class MailBoxProjection
    {
        public static Expression<Func<MailBox, MailBox>> MailBoxInformation => o => new MailBox
        {
            Id = o.Id,
            Server = o.Server,
            FromAddress = o.FromAddress,
            AdminEmail = o.AdminEmail,
            ServerUserName = o.ServerUserName,
            ServerPassword = o.ServerPassword,
            ConnectionSecurity = o.ConnectionSecurity,
            CreatedBy = o.CreatedBy,
            CreatedDate = o.CreatedDate,
            ModifiedBy = o.ModifiedBy,
            ModifiedDate = o.ModifiedDate,
        };
    }
}
