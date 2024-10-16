using Infrastructure.Common.Events;
using System;

namespace Infrastructure.Common.Eventstores.Projection
{
    public interface IProjection
    {
        Type[] Handles { get; }
        void Handle(IEvent @event);
    }
}
