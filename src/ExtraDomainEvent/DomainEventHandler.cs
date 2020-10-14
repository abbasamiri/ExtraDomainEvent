using System.Threading.Tasks;
using ExtraDomainEvent.Abstractions;

namespace ExtraDomainEvent
{
    /// <summary>
    /// Represents a domain-event-handler.
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    /// <typeparam name="TDomainEventContext"></typeparam>
    public abstract class
        DomainEventHandler<TDomainEvent, TDomainEventContext> : IDomainEventHandler<TDomainEvent, TDomainEventContext>
        where TDomainEvent : IDomainEvent
        where TDomainEventContext : IDomainEventContext
    {
        /// <summary>
        /// Handles a domain-event.
        /// </summary>
        /// <param name="domainEvent">The type of the domain-event.</param>
        /// <param name="domainEventContext">the instance of domain-event-context.</param>
        /// <returns>A task representing the result of invoking the handler.</returns>
        public abstract Task<DispatchResult> Handle(
            TDomainEvent domainEvent,
            TDomainEventContext domainEventContext);
    }
}