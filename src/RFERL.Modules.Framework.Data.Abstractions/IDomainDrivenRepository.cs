using System;

namespace RFERL.Modules.Framework.Data.Abstractions
{
    /// <summary>Repository interface specific for domain driven design.</summary>
    /// <typeparam name="TEntity">Type of entity which is aggregate root.</typeparam>
    /// <typeparam name="TKey">Type of entity's ID.</typeparam>
    public interface IDomainDrivenRepository<TEntity, in TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>, IAggregateRoot
        where TKey : IComparable
    {
    }
}