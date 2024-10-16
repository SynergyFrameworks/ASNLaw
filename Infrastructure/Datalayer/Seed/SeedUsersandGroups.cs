using Datalayer.Domain.Group;
using Datalayer.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datalayer.Seed
{
    public static class SeedCapUsersandGroups
    {
        public static void EnsureUserGroupsSeeded(this Context.ASNDbContext context)
        {
            var groupId = Guid.NewGuid();
            var emmanuelUserId = Guid.Parse("0a217f14-e836-4e8d-8b4b-7322e7ebda0e");
            var aaronsUserId = Guid.Parse("d72bf14f-7a91-4b1b-a90a-36d5a1144c78");
            var charlesUserId = Guid.Parse("4ad967ba-ef62-44c6-9eca-3134de1c0e01");

            List <Guid> IdentityUsers = new List<Guid>()
            { emmanuelUserId, aaronsUserId, charlesUserId
            };

            if (!context.Users.Any(x => 
                x.IdentityUserId == IdentityUsers.ElementAt(0) || 
                x.IdentityUserId == IdentityUsers.ElementAt(1) || 
                x.IdentityUserId == IdentityUsers.ElementAt(2))
                )
            {

                context.Users.AddRange(AddCapUsers(IdentityUsers, context));
                context.SaveChanges();
            }
        }
        private static User[] AddCapUsers( List<Guid> IdentityUsers, Context.ASNDbContext context)
        {
            var managementGroupGId = Guid.NewGuid();
            var managementPermissions = context.Permissions.Where(x => x.AspNetRole == "Manager").ToList();
            var managementGroupPermissions = new List<GroupPermission>();
            foreach(var perm in managementPermissions)
            {
                managementGroupPermissions.Add(new GroupPermission()
                {
                    Id = Guid.NewGuid(),
                    PermissionId = perm.Id,
                    GroupId = managementGroupGId,
                    Enabled = true,
                    CreatedBy = "system",
                    CreatedDate = DateTime.UtcNow,

                });
            }
           
            var redTeam = Guid.Parse("4E05825C-7B5C-413E-BB01-5C700E9A66EE");
            var managementGroup = new Domain.Group.ASNGroup()
            {
                Id = managementGroupGId,
                Name = "Management",
                Description = "Manangement ASNGroup",
                ImageUrl = "test.png",
                TeamId = redTeam,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow,
                GroupPermissions = managementGroupPermissions
            };

            return new User[] {
                    new User {
                        IdentityUserId = IdentityUsers.ElementAt(0),
                        UserName = "etoledo",
                        Name = "Emmanuel Toledo",
                        Email = "etoledo@yahoo.com",
                        ImageUrl = "http://google.com/google.png",
                        IsActive = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Groups = new List<ASNGroup>(){
                            managementGroup 
                        },

                    },
                    new User {
                        IdentityUserId = IdentityUsers.ElementAt(1),
                        UserName = "aprince",
                        Name = "Aaron Prince",
                        Email = "aprince@yahoo.com",
                        ImageUrl = "http://google.com/google.png",
                        IsActive = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Groups = new List<ASNGroup>(){
                            managementGroup
                        },
                    },
                    new User {
                        IdentityUserId = IdentityUsers.ElementAt(2),
                        UserName = "cdalesch",
                        Name = "Charles Dalesch",
                        Email = "cdalesch@yahoo.com",
                        ImageUrl = "http://google.com/google.png",
                        IsActive = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Groups = new List<ASNGroup>(){
                            managementGroup
                        },
                    },
            };
        }
    }
}
