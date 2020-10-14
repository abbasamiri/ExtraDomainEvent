using System;

namespace ExtraDomainEvent.Policies
{
    [Flags]
    public enum PublishPolicy
    {
        /// <summary>
        /// No policy should be applied.
        /// </summary>
        None,
        /// <summary>
        /// Prevent duplicate type.
        /// </summary>
        PreventDuplicateType,
        /// <summary>
        /// Prevent duplicate reference.
        /// </summary>
        PreventDuplicateReference,
    }
}