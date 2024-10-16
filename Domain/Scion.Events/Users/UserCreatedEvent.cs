using FluentValidation;
using ASNDatalayer.Contracts;
using ASNInfrastructure.Common;
using ASNInfrastructure.Events;
using System;
using System.Security.Principal;

namespace ASNEvents.Users
{
    public class UserCreatedEvent : IIdentity, IEvent
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTimeOffset TimeStamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string AuthenticationType => throw new NotImplementedException();

        public bool IsAuthenticated => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        Guid IEntity.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        

        public class Validator : AbstractValidator<UserCreatedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.UserId).NotEmpty();
                RuleFor(e => e.FirstName).NotEmpty();
                RuleFor(e => e.LastName).NotEmpty();
                RuleFor(e => e.Email).NotEmpty().EmailAddress();
            }
        }
    }
}
