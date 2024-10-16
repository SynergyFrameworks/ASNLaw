using Datalayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Projections
{
    public class TeamProjection
    {
        public static Expression<Func<Team, Team>> TeamInformation
        {
            get =>
                o => new Team
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    ClientId = o.ClientId,
                    Client = o.Client,
                    Groups = o.Groups
                };
        }
    }
}
