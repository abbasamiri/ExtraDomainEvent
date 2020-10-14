using System;
using System.Threading.Tasks;
using ExtraDomainEvent.Policies;

namespace ExtraDomainEvent.Abstractions
{
    /// <summary>
    /// Defines a dispatcher for the domain-event(s).
    /// </summary>
    public interface IDomainEventDispatcher : IDisposable
    {
        /// <summary>
        /// Dispatches all domain-events.
        /// </summary>
        /// <param name="context">The instance of domain-event-context.</param>
        /// <param name="dispatchPolicy">The dispatch policy.</param>
        /// <typeparam name="TDomainEventContext">The type of domain-event-context.</typeparam>
        /// <returns>A task representing invoking all handlers.</returns>
        Task<DispatchResult[]> Dispatch<TDomainEventContext>(TDomainEventContext context, DispatchPolicy dispatchPolicy);
    }
}