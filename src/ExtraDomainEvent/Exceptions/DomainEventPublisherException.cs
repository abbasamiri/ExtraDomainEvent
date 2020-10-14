using System;

namespace ExtraDomainEvent.Exceptions
{
    /// <summary>
    /// Represents errors that occur during domain-event publishing. 
    /// </summary>
    public class DomainEventPublisherException: Exception
    {
        /// <summary>
        /// Initializes a new instance of <strong>DomainEventPublisherException</strong> class.
        /// </summary>
        public DomainEventPublisherException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <strong>DomainEventPublisherException</strong> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DomainEventPublisherException(string message): base(message)    
        {
            
        }

        /// <summary>
        /// Initializes a new instance of <strong>DomainEventPublisherException</strong> class with a specified error message and a reference to the inner exception that is the cause of this exception.   
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DomainEventPublisherException(string message, Exception innerException): base(message, innerException)    
        {
        }
    }
}