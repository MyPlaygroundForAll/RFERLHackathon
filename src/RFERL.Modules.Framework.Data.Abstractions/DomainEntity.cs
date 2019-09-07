using System;
using System.Collections.Generic;
using MediatR;

namespace RFERL.Modules.Framework.Data.Abstractions
{
    /// <summary>Entity abstraction for domain driven design modeling.</summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public abstract class DomainEntity<TEntity> : Entity<TEntity>, IHasDomainEvents
        where TEntity : IComparable
    {
        /// <summary>Backing field for <see cref="DomainEvents"/>.</summary>
        private List<INotification> _domainEvents;

        /// <summary>Gets domain events which are registered for entity.</summary>
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>Adds a domain event to entity.</summary>
        /// <param name="eventItem">Event which is going to be registered.</param>
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        /// <summary>Removes domain event from entity.</summary>
        /// <param name="eventItem">Event which is going to be removed.</param>
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        /// <summary>Clears all registered domain events.</summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}