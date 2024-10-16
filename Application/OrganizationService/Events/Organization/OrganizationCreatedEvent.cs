using FluentValidation;
using Datalayer.Contracts;
using Infrastructure.Common.Events;
using System;

namespace OrganizationService.Events.Organizaton
{
    public class OrganizationCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTimeOffset TimeStamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

        public class Validator : AbstractValidator<OrganizationCreatedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.Id).NotEmpty();
                RuleFor(e => e.CompanyName).NotEmpty();
       
            }
        }
 }

