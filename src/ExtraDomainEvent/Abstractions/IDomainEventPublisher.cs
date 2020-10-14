using System;
using System.Collections.Generic;

namespace ExtraDomainEvent.Abstractions
{
    /// <summary>
    /// Defines a publisher for domain-event(s).
    /// </summary>
    public interface IDomainEventPublisher : IDisposable
    {
        /// <summary>
        /// Publishes a domain-event.
        /// </summary>
        /// <param name="event">The instance of domain-event.</param>
        void Publish(IDomainEvent @event);
        
        /// <summary>
        /// Publishes multiple domain-events.
        /// </summary>
        /// <param name="events">The collection of domain-events to handle.</param>
        void Publish(IEnumerable<IDomainEvent> events);
        
        /// <summary>
        /// Removes all domain-events waiting to be handled. 
        /// </summary>
        void ClearEvents();

        /// <summary>
        /// Removes a domain-event from the collection.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        bool RemoveEvent(IDomainEvent @event);
        
        /// <summary>
        /// Get all domain-events waiting to be handled.
        /// </summary>
        /// <returns>The collection of domain-events waiting to handle.</returns>
        IEnumerable<IDomainEvent> GetEvents();
    }
}