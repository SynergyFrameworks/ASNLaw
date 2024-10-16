using FluentValidation;
using Infrastructure.Common.Events;
using System;

namespace OrganizationService.Events.Organizaton
{
    public class OrganizationDeletedEvent : Event
    {
        public Guid UserId { get; set; }

        public class Validator : AbstractValidator<OrganizationDeletedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.UserId).NotEmpty();
            }
        }
    }
}
