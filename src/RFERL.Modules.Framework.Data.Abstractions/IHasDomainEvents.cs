using System.Collections.Generic;
using MediatR;

namespace RFERL.Modules.Framework.Data.Abstractions
{
    /// <summary>Interface for implementing domain events to entity.</summary>
    public interface IHasDomainEvents
    {
        /// <summary>Gets domain events which are registered for entity.</summary>
        IReadOnlyCollection<INotification> DomainEvents { get; }

        /// <summary>Adds a domain event to entity.</summary>
        /// <param name="eventItem">Event which is going to be registered.</param>
        void AddDomainEvent(INotification eventItem);

        /// <summary>Removes domain event from entity.</summary>
        /// <param name="eventItem">Event which is going to be removed.</param>
        void RemoveDomainEvent(INotification eventItem);

        /// <summary>Clears all registered domain events.</summary>
        void ClearDomainEvents();
    }
}
