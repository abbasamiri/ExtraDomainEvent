using System.Threading.Tasks;

namespace ExtraDomainEvent.Abstractions
{
    /// <summary>
    /// Defines a handler for a domain-event.
    /// </summary>
    /// <typeparam name="TDomainEvent">The type of domain-event being handled.</typeparam>
    /// <typeparam name="TDomainEventContext">The type of domain-event-context being passed to the handler.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent, in TDomainEventContext>
        where TDomainEvent : IDomainEvent
        where TDomainEventContext : IDomainEventContext
    {
        /// <summary>
        /// Handles the domain-event.
        /// </summary>
        /// <param name="domainEvent">The domain-event to be handled.</param>
        /// <param name="domainEventContext">The instance of domain-event-context.</param>
        /// <returns>A task containing the result of handling the domain-event.</returns>
        Task<DispatchResult> Handle(TDomainEvent domainEvent, TDomainEventContext domainEventContext);
    }
}