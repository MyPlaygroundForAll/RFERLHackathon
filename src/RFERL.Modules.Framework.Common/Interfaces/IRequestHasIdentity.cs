using System;

namespace RFERL.Modules.Framework.Common.Interfaces
{
    /// <summary>Interface for marking request has identity.</summary>
    /// <typeparam name="TKey">Type of identity property.</typeparam>
    public interface IRequestHasIdentity<TKey>
        where TKey : IComparable
    {
        /// <summary>Gets or sets iD of request.</summary>
        TKey Id { get; set; }
    }
}