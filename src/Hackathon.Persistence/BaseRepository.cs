using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RFERL.Modules.Framework.Data.Abstractions;

namespace Hackathon.Persistence.Repositories
{
    /// <summary>Represents base data accessor for all database objects.</summary>
    /// <typeparam name="TContext">Type of database context.</typeparam>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of key.</typeparam>
    public class BaseRepository<TContext, TEntity, TKey> : IDomainDrivenRepository<TEntity, TKey>
        where TContext : DbContext, IUnitOfWork
        where TEntity : Entity<TKey>, IAggregateRoot
        where TKey : IComparable
    {
        private readonly TContext _context;

        /// <summary>Initializes a new instance of the <see cref="BaseRepository{TContext, TEntity, TKey}"/> class.</summary>
        /// <param name="context">Database context instance.</param>
        protected BaseRepository(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Dbset = _context.Set<TEntity>();
        }

        /// <summary>Gets unit of work instance. <see cref="IUnitOfWork"/>.</summary>
        public IUnitOfWork UnitOfWork => _context;

        /// <summary>Gets Dbset for related entity.</summary>
        protected DbSet<TEntity> Dbset { get; }

        /// <summary>Adds new entity.</summary>
        /// <param name="entity">Object to be added.</param>
        public void Add(TEntity entity)
        {
            Dbset.Add(entity);
        }

        /// <summary>Find entities which matches with given specification.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>List of entities.</returns>
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> spec)
        {
            return Dbset.Where(spec).ToList();
        }

        /// <summary>Async version of <see cref="IRepository{TEntity,TKey}.Find"/>.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>List of entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> spec)
        {
            return await Dbset.Where(spec).ToListAsync();
        }

        /// <summary>Finds entity by ID.</summary>
        /// <param name="id">ID of entity.</param>
        /// <returns>If it's found then returns entity, else return null.</returns>
        public virtual TEntity FindById(TKey id)
        {
            return Dbset.FirstOrDefault(e => e.Id.Equals(id));
        }

        /// <summary>Async version of <see cref="IRepository{TEntity,TKey}.FindById"/>.</summary>
        /// <param name="id">ID of entity.</param>
        /// <returns>If it's found then returns entity, else return null.</returns>
        public virtual async Task<TEntity> FindByIdAsync(TKey id)
        {
            return await Dbset.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        /// <summary>Find single entity which matches with given specification.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>If it's found then returns entity, else return null.</returns>
        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> spec)
        {
            return Dbset.FirstOrDefault(spec);
        }

        /// <summary>Async version of <see cref="IRepository{TEntity,TKey}.FindOne"/>.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>If it's found then returns entity, else return null.</returns>
        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> spec)
        {
            return Dbset.FirstOrDefaultAsync(spec);
        }

        /// <summary>Checks any record matches given specification.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>If any record found then true, else false.</returns>
        public bool Any(Expression<Func<TEntity, bool>> spec)
        {
            return Dbset.Any(spec);
        }

        /// <summary>Async version of <see cref="IRepository{TEntity,TKey}.Any"/>.</summary>
        /// <param name="spec">Specification which contains filtering options.</param>
        /// <returns>If any record found then true, else false.</returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> spec)
        {
            return await Dbset.AnyAsync(spec);
        }

        /// <summary>Removes existing entity.</summary>
        /// <param name="entity">Object to be removed.</param>
        public void Remove(TEntity entity)
        {
            Dbset.Remove(entity);
        }

        /// <summary>Updates existing entity.</summary>
        /// <param name="entity">Object to be updated.</param>
        public void Update(TEntity entity)
        {
            Dbset.Update(entity);
        }
    }
}
