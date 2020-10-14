namespace ExtraDomainEvent.Policies
{
    /// <summary>
    /// Represents the policy to dispatch multiple events.
    /// </summary>
    public enum DispatchPolicy
    {
        /// <summary>
        /// Ignores Failure and continue.
        /// </summary>
        IgnoreFailureAndContinue,
        
        /// <summary>
        /// First Failure terminates dispatching process.  
        /// </summary>
        TerminateOnFirstFailure,
    }
}