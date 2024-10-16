using System;

namespace Infrastructure.Common.Domain
{
    public interface IUpdateObject
    {
        string Name { get; set; }
        Guid Id { get; set; }
        string Application { get; set; }
        bool IsSearchDriven { get; set; }
    }
}