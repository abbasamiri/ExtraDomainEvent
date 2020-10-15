using System;
using ExtraDomainEvent.Abstractions;

namespace ExtraDomainEvent.Exceptions
{
    /// <summary>
    /// Represents errors that occur during domain-event dispatch. 
    /// </summary>
    public class DomainEventException : Exception
    {
        public IDomainEvent DomainEvent { get; }

        /// <summary>
        /// Initializes a new instance of <strong>DomainEventDispatchException</strong> class.
        /// </summary>
        /// <param name="event">The domain-event instance.</param>
        public DomainEventException(IDomainEvent @event)
        {
            DomainEvent = @event;
        }

        /// <summary>
        /// Initializes a new instance of <strong>DomainEventDispatchException</strong> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="event">The domain-event instance.</param>
        public DomainEventException(string message, IDomainEvent @event) : base(message)
        {
            DomainEvent = @event;
        }

        /// <summary>
        /// Initializes a new instance of <strong>DomainEventDispatchException</strong> class with a specified error message and a reference to the inner exception that is the cause of this exception.   
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="event">The domain-event instance.</param>
        public DomainEventException(string message, Exception innerException, IDomainEvent @event) : base(
            message, innerException)
        {
            DomainEvent = @event;
        }
    }
}