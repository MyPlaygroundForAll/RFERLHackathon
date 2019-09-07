using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RFERL.Modules.Framework.Data.Abstractions
{
    /// <summary>Common repository interface for accessing data storage.</summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity's ID.</typeparam>
    public interface IRepository<TEntity, in TKey>
        where TEntity : Entity<TKey>
        where TKey : IComparable
    {
        /// <summary>Gets unit of work instance. <see cref="IUnitOfWork"/>.</summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>Finds entity by ID.</summary>
        /// <param name="id">ID of entity.</param>
        /// <returns>If it's found then returns entity, else return null.</returns>
        TEntity FindById(TKey id);

        /// <summary>Async version of <see cref="FindById(TKey)"/>.</summary>
        /// <param name="id">ID of entity.</param>
        /// <returns>If it's found then returns entity, else return null.</returns>
        Task<TEntity> FindByIdAsync(TKey id);

        /// <summary>Find single entity which matches with given specification.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>If it's found then returns entity, else return null.</returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> spec);

        /// <summary>Async version of <see cref="FindOne"/>.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>If it's found then returns entity, else return null.</returns>
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> spec);

        /// <summary>Find entities which matches with given specification.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>List of entities.</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> spec);

        /// <summary>Async version of <see cref="Find"/>.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>List of entities.</returns>
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> spec);

        /// <summary>Checks any record matches given specification.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>If any record found then true, else false.</returns>
        bool Any(Expression<Func<TEntity, bool>> spec);

        /// <summary>Async version of <see cref="Any"/>.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>If any record found then true, else false.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> spec);

        /// <summary>Adds new entity.</summary>
        /// <param name="entity">Object to be added.</param>
        void Add(TEntity entity);

        /// <summary>Updates existing entity.</summary>
        /// <param name="entity">Object to be updated.</param>
        void Update(TEntity entity);

        /// <summary>Removes existing entity.</summary>
        /// <param name="entity">Object to be removed.</param>
        void Remove(TEntity entity);
    }
}