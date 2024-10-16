using Datalayer.Contracts;
using Datalayer.Domain.Group;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Security
{
    [Table("GroupPermissions")]
    public class GroupPermission: Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Group))]
        public Guid GroupId { get; set; }

        [ForeignKey(nameof(Permission))]
        public Guid PermissionId { get; set; }

        public bool Enabled { get; set; }

        public ASNGroup Group { get; set; }

        public Permission Permission { get; set; }
    }
}
