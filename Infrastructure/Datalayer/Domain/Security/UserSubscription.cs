using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Security
{
    [Table("UserSubscriptions")]
    public class UserSubscription : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Column("SubscriptionId")]
        [Required]
        [ForeignKey(nameof(Subscription))]
        public Guid SubscriptionId { get; set; }

        [Required]
        public int SubscriptionLevel { get; set; }

        public Subscription Subscription { get; set; }

        public User User { get; set; }
    }
}
