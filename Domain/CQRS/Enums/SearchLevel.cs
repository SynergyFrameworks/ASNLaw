using System;

namespace Infrastructure.CQRS.Enums
{
    [Flags]
    public enum SearchLevel
    {
        Simple = 1,
        Full = 2
    }

}
