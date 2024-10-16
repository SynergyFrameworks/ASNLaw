using Infrastructure.Common.Eventstores.Aggregate;
using System;

namespace Infrastructure.Common.Eventstores.Snapshot
{
    public interface ISnapshot
    {
        Type Handles { get; }
        void Handle(IAggregate aggregate);
    }
}
