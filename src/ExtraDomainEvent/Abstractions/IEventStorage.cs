using System;
using System.Collections.Generic;

namespace ExtraDomainEvent.Abstractions
{
    /// <summary>
    /// Defines a <strong>List</strong> to store published domain-events.   
    /// </summary>
    public interface IEventStorage : IList<IDomainEvent>, IDisposable
    {
    }
}