using FluentValidation;
using ASN.Infrastructure.Events;
using System;
using static ASN.Business.Parse.Model.ParseTypeDef;

namespace ASN.Events.Parse
{
    public class ParseCompletedEvent : Event
    {

        public Guid ParseId { get; init; }
        public ParseType ParseType { get; init; }
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TenantId { get; set; }
        public int DateTimeUTC { get; set; }

        public class Validator : AbstractValidator<ParseCompletedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.ParseId).NotEmpty();
                RuleFor(e => e.ParseType).NotEmpty();
                RuleFor(e => e.UserId).NotEmpty();
                RuleFor(e => e.TaskId).NotEmpty();
                RuleFor(e => e.ProjectId).NotEmpty();
                RuleFor(e => e.TenantId).NotEmpty();
                RuleFor(e => e.DateTimeUTC).GreaterThanOrEqualTo(2020);
            }
        }
    }
}
