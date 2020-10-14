using System;

namespace ExtraDomainEvent.Exceptions
{
    /// <summary>
    /// Represents errors that occur during domain-event dispatch. 
    /// </summary>
    public class DomainEventDispatcherException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <strong>DomainEventDispatcherException</strong> class.
        /// </summary>
        public DomainEventDispatcherException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <strong>DomainEventDispatcherException</strong> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DomainEventDispatcherException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <strong>DomainEventDispatcherException</strong> class with a specified error message and a reference to the inner exception that is the cause of this exception.   
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DomainEventDispatcherException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}