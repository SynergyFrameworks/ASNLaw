using Infrastructure.Common;
using System;

namespace Infrastructure.Common.Domain.Contracts
{
    /// <summary>
    /// An interface for basic entity (data-layer) implementation.
    /// Entity-model conversion and vice-versa 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
