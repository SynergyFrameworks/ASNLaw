using System;
using Scion.Domain.UserStatus;

namespace Scion.Domain.Users
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public UserStatusRef UserStatusRef { get; set; }
        public UserStatusEnum Status { get; set; } = UserStatusEnum.WaitingConfirmation;

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;       
    }
}
