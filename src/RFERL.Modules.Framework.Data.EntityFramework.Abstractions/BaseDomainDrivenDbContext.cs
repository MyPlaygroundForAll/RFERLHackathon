using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RFERL.Modules.Framework.Data.Abstractions;

namespace RFERL.Modules.Framework.Data.EntityFramework.Abstractions
{
    /// <summary>Base implementation of data context for domain driven design.</summary>
    public abstract class BaseDomainDrivenDbContext : BaseDbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        /// <summary>Constructor for <see cref="BaseDomainDrivenDbContext"/>.</summary>
        /// <param name="options">Database context options <see cref="DbContextOptions"/>.</param>
        /// <param name="mediator">Mediator instance for publishing domain events.</param>
        protected BaseDomainDrivenDbContext(DbContextOptions options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>Save entities on data context. (Used for dispatching domain events.)</summary>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
        /// <returns>Return true if operation is successful, else false.</returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventsAsync(this);
            return true;
        }
    }
}