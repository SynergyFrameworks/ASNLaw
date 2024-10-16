using Infrastructure.Common.Eventstores.Aggregate;
using Parse.Service.Events;
using ParseService.Events;
using System;
using static Domain.Parse.Model.ParseTypeDef;

namespace ParseService
{
    public class ParseAggregate : Aggregate
    {

        public Guid ParseId { get; set; }
        public ParseType ParseType { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TenantId { get; set; }
        public DateTime DateTimeUTC { get; set; }


        protected ParseAggregate()
        {

        }

        private ParseAggregate(Guid taskId, Guid projectId, Guid userId, Guid tenantId)
        {
            var @event = new ParseCreatedEvent
            {
                ParseId = Guid.NewGuid(),
                UserId = userId,
                TaskId = taskId,
                ProjectId = projectId,
                TenantId = tenantId
            };

            Enqueue(@event);
            Apply(@event);
        }

        public static ParseAggregate CreateParse(Guid taskId, Guid projectId, Guid userId, Guid tenantId)
        {
            return new ParseAggregate(taskId, projectId, userId, tenantId);
        }

        public void DeleteParse(Guid userId)
        {
            var @event = new ParseDeletedEvent
            {
                ParseId = Id,
                UserId = userId
            };

            Enqueue(@event);
        }

        private void Apply(ParseCreatedEvent @event)
        {
            ParseId = @event.ParseId;
            UserId = @event.UserId;
            TaskId = @event.TaskId;
            ProjectId = @event.ProjectId;
            TenantId = @event.TenantId;
        }
    }
}
