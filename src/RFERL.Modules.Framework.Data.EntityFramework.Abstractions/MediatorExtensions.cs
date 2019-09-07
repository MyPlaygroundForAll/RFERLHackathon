using System.Linq;
using System.Threading.Tasks;
using MediatR;
using RFERL.Modules.Framework.Data.Abstractions;

namespace RFERL.Modules.Framework.Data.EntityFramework.Abstractions
{
    /// <summary>Provides extensions for Mediator.</summary>
    public static class MediatorExtensions
    {
        /// <summary>Finds changes in tracked entities which implements domain events and dispatch event.</summary>
        /// <param name="mediator">Mediator instance for publishing domain events.</param>
        /// <param name="context">Database context instance.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BaseDomainDrivenDbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<IHasDomainEvents>()
                .Where(x => x.Entity.DomainEvents?.Any() == true).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => await mediator.Publish(domainEvent).ConfigureAwait(false));

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
