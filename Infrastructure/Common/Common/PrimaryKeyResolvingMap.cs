using System.Collections.Generic;
using Infrastructure.Common.Domain.Contracts;

namespace Infrastructure.Common
{
    /// <summary>
    /// Helper class used for resolving model object primary keys when it presisted in persistent infrastructure
    /// Used in model to db model converters
    /// </summary>
    public class PrimaryKeyResolvingMap 
    {
        private readonly Dictionary<IEntity, IEntity> _resolvingMap = new Dictionary<IEntity, IEntity>();
   
        public void AddPair(IEntity transientEntity, IEntity persistentEntity)
        {
            _resolvingMap[transientEntity] = persistentEntity;
        }

        public void ResolvePrimaryKeys()
        {
            foreach (var pair in _resolvingMap)
            {
                if (string.IsNullOrEmpty(pair.Key.Id.ToString()) && !string.IsNullOrEmpty(pair.Value.Id.ToString()))
                {
                    pair.Key.Id = pair.Value.Id;

                    if (pair.Key is IAuditable transientAuditable && pair.Value is IAuditable persistentAuditable)
                    {
                        transientAuditable.CreatedBy = persistentAuditable.CreatedBy;
                        transientAuditable.CreatedDate = persistentAuditable.CreatedDate;
                        transientAuditable.ModifiedBy = persistentAuditable.ModifiedBy;
                        transientAuditable.ModifiedDate = persistentAuditable.ModifiedDate;
                    }
                }
            }
        }
    }
}
