using System;
using System.Collections.Generic;

namespace RFERL.Modules.Framework.Data.Abstractions
{
    /// <summary>Custom implementation of <see cref="Enum"/> for providing extra functionalities and benefits.</summary>
    public abstract class Enumeration : IComparable
    {
        /// <summary>Initializes a new instance of the <see cref="Enumeration"/> class.</summary>
        /// <param name="id">Key value of enumeration.</param>
        /// <param name="name">Name value of enumeration.</param>
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>Gets key of Enumeration.</summary>
        public int Id { get; private set; }

        /// <summary>Gets name. </summary>
        public string Name { get; private set; }

        /// <summary>Equality operator for enumeration.</summary>
        /// <param name="left">Left side of expression.</param>
        /// <param name="right">Right side of expression.</param>
        /// <returns>If equals true, else false.</returns>
        public static bool operator ==(Enumeration left, Enumeration right)
        {
            return left?.Equals(right) ?? ReferenceEquals(right, null);
        }

        /// <summary>Greater than operator for enumeration.</summary>
        /// <param name="left">Left side of expression.</param>
        /// <param name="right">Right side of expression.</param>
        /// <returns>If greater than true, else false.</returns>
        public static bool operator >(Enumeration left, Enumeration right)
        {
            return left.Id.CompareTo(right.Id) > 0;
        }

        /// <summary>Greater than or equal operator for enumeration.</summary>
        /// <param name="left">Left side of expression.</param>
        /// <param name="right">Right side of expression.</param>
        /// <returns>If greater than or equal true, else false.</returns>
        public static bool operator >=(Enumeration left, Enumeration right)
        {
            return left.Id.CompareTo(right.Id) >= 0;
        }

        /// <summary>Less than operator for enumeration.</summary>
        /// <param name="left">Left side of expression.</param>
        /// <param name="right">Right side of expression.</param>
        /// <returns>If less than true, else false.</returns>
        public static bool operator <(Enumeration left, Enumeration right)
        {
            return left.Id.CompareTo(right.Id) < 0;
        }

        /// <summary>Less than or equal operator for enumeration.</summary>
        /// <param name="left">Left side of expression.</param>
        /// <param name="right">Right side of expression.</param>
        /// <returns>If less than or equal true, else false.</returns>
        public static bool operator <=(Enumeration left, Enumeration right)
        {
            return left.Id.CompareTo(right.Id) <= 0;
        }

        /// <summary>Not equals operator for enumeration.</summary>
        /// <param name="left">Left side of expression.</param>
        /// <param name="right">Right side of expression.</param>
        /// <returns>If not equals true, else false.</returns>
        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !(left == right);
        }

        /// <summary>Converts object to string.</summary>
        /// <returns>Name value of enumeration.</returns>
        public override string ToString() => Name;

        /// <summary>Custom equals implementation for enumeration.</summary>
        /// <param name="obj">Compared object.</param>
        /// <returns>If compared object is same type and also has same ID value then true, else false.</returns>
        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;
            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);
            return typeMatches && valueMatches;
        }

        /// <summary>Custom CompareTo implementation for enumeration.</summary>
        /// <param name="obj">Compared object.</param>
        /// <returns>Comparison result for ID value of enumerations.</returns>
        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);

        /// <summary>Gets hashcode of enumeration object.</summary>
        /// <returns>Hashcode for Enumeration object.</returns>
        public override int GetHashCode()
        {
            var hashCode = -1919740922;
            hashCode = (hashCode * -1521134295) + Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}