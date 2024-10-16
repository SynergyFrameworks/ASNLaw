using System;

namespace Infrastructure.Common.Persistence
{
    public interface IPersistent
    {
        Guid Id { get; set; }
    }
}
