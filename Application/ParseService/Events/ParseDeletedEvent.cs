using FluentValidation;
using Infrastructure.Common.Events;
using System;


namespace Parse.Service.Events
{
    public class ParseDeletedEvent : Event
    {
        public Guid ParseId { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public int DateTimeUTC { get; set; }

        public class Validator : AbstractValidator<ParseDeletedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.ParseId).NotEmpty();
                RuleFor(e => e.UserId).NotEmpty();
                RuleFor(e => e.TenantId).NotEmpty();
                RuleFor(e => e.DateTimeUTC).GreaterThanOrEqualTo(2020);
            }
        }
    }
}
