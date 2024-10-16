using Datalayer.Domain;
using System;
using System.Linq.Expressions;
using Infrastructure.CQRS.Models;

namespace Infrastructure.CQRS.Projections
{
    public static class OrganizationProjection
    {
        public static Expression<Func<Organization, Organization>> OrganizationInformation
        {
            get =>
                org => new Organization
                {
                    Id = org.Id,
                    Description = org.Description,
                    Name = org.Name,
                    ImageUrl = org.ImageUrl,
                    WebUrl = org.WebUrl,
                    User = org.User
                };
        }

    }
}
