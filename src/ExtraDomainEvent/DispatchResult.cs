using System;

namespace ExtraDomainEvent
{
    /// <summary>
    /// Represents the result of handling a domain-event.
    /// </summary>
    public class DispatchResult
    {
        /// <summary>
        /// Returns <strong>true</strong> if a domain-event has been handled successfully. 
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Returns the error occurs during handling domain-event.
        /// </summary>
        public Exception Error { get; }

        /// <summary>
        /// Returns <strong>false</strong> if a domain-event has not been handled successfully.
        /// </summary>
        public bool Failure => !Success;
        
        /// <summary>
        /// The protected constructor. 
        /// </summary>
        /// <param name="success">The result.</param>
        /// <param name="error">The error if the result is false.</param>
        protected DispatchResult(bool success, Exception error)
        {
            Success = success;
            Error = error;
        }

        /// <summary>
        /// Result of handling a domain-event. 
        /// </summary>
        /// <param name="exception">The error that occurs during handling domain-event.</param>
        /// <returns>An instance of <strong>DispatchResult</strong>.</returns>
        public static DispatchResult Fail(Exception exception)
        {
            return new DispatchResult(false, exception);
        }

        /// <summary>
        /// Result of handling a domain-event.
        /// </summary>
        /// <returns>An instance of <strong>DispatchResult</strong>.</returns>
        public static DispatchResult Ok()
        {
            return new DispatchResult(true, null);
        }

        /// <summary>
        /// Combines multiple <strong>DispatchResult</strong>.
        /// </summary>
        /// <param name="results">An array of <strong>DispatchResults</strong>.</param>
        /// <returns>An instance of <strong>DispatchResult</strong>.</returns>
        public static DispatchResult Combine(params DispatchResult[] results)
        {
            foreach (var result in results)
            {
                if (result.Failure)
                    return result;
            }

            return Ok();
        }
    }
}