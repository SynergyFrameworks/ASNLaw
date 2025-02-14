using System;
using Infrastructure.Common.Domain.Contracts;

namespace Infrastructure.Common
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }

        public bool IsTransient()
        {
            return Id == Guid.Empty;
        }

        #region Overrides Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (ReferenceEquals(null, obj))
                return false;

            if (GetRealObjectType(this) != GetRealObjectType(obj))
                return false;


            var other = obj as Entity;
            return other != null && Id == other.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {

#pragma warning disable S3249 // Classes directly extending "object" should not call "base" in "GetHashCode" or "Equals"
                // For IEntities without Id we want to use object GetHashCode
                return IsTransient() ? base.GetHashCode() : Id.GetHashCode();
#pragma warning restore S3249 // Classes directly extending "object" should not call "base" in "GetHashCode" or "Equals"
            }
        }
        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !Equals(left, right);
        }

        #endregion

        private Type GetRealObjectType(object obj)
        {
            var retVal = obj.GetType();
            //because can be compared two object with same id and 'types' but one of it is EF dynamic proxy type)
            if (retVal.BaseType != null && retVal.Namespace == "System.Data.Entity.DynamicProxies")
            {
                retVal = retVal.BaseType;
            }
            return retVal;
        }
    }
}
