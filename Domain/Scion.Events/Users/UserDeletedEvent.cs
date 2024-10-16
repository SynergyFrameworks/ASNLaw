using FluentValidation;
using ASN.Infrastructure.Events;
using System;

namespace ASN.Events.Users
{
    public class UserDeletedEvent : Event
    {
        public Guid UserId { get; set; }

        public class Validator : AbstractValidator<UserDeletedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.UserId).NotEmpty();
            }
        }
    }
}
