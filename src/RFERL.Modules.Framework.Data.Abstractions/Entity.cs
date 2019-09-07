using System;

namespace RFERL.Modules.Framework.Data.Abstractions
{
    /// <summary>Entity abstraction for modeling data.</summary>
    /// <typeparam name="T">Type of ID.</typeparam>
    public abstract class Entity<T>
        where T : IComparable
    {
        /// <summary>Gets iD of object.</summary>
        public virtual T Id { get; protected set; }

        /// <summary>Checks object is transient (not persisted to database) or not.</summary>
        /// <returns>Returns true if object is transient, if not false.</returns>
        public bool IsTransient()
        {
            var defaultValue = default(T);
            return defaultValue == null || defaultValue.Equals(Id);
        }

        /// <summary>Checks item is equal to compared item.</summary>
        /// <param name="obj">Object to compare with item.</param>
        /// <returns>Return true if both objects are same, if not false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Entity<T>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var item = (Entity<T>)obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id.Equals(Id);
            }
        }

        /// <summary>Gets Hashcode of item.</summary>
        /// <returns>Hashcode of item.</returns>
        public override int GetHashCode()
        {
            // Overflow is fine, just wrap
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ Id.GetHashCode();
                return hash;
            }
        }
    }
}
