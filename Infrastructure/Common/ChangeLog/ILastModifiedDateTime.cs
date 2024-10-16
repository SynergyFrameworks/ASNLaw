using System;

namespace Infrastructure.Common.ChangeLog
{
    public interface ILastModifiedDateTime
    {
        DateTimeOffset LastModified { get; }
        void Reset();
    }
}
