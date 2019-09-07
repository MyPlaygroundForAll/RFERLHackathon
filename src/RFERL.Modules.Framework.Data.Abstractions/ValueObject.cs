using System.Collections.Generic;
using System.Linq;

namespace RFERL.Modules.Framework.Data.Abstractions
{
    /// <summary>Represents value object in domain driven design.</summary>
    public abstract class ValueObject
    {
        /// <summary>Compares values object by using <see cref="GetAtomicValues"/>.</summary>
        /// <param name="obj">Compared value object.</param>
        /// <returns>If they are equal then true, else false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;
            var thisValues = GetAtomicValues().GetEnumerator();
            var otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        /// <summary>Gets hashcode of value object.</summary>
        /// <returns>Hashcode of object.</returns>
        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>Copies value object.</summary>
        /// <returns>Copied instance of value object.</returns>
        public ValueObject GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }

        /// <summary>Defines fields which needs to be checked for comparing two value objects.</summary>
        /// <returns>List of value's of fields.</returns>
        protected abstract IEnumerable<object> GetAtomicValues();
    }
}