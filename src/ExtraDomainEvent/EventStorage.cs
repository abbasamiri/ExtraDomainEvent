using System.Collections.Generic;
using ExtraDomainEvent.Abstractions;

namespace ExtraDomainEvent
{
    /// <summary>
    /// The default implementation of <see cref="IEventStorage"/>
    /// </summary>
    public class EventStorage : List<IDomainEvent>, IEventStorage
    {
        public EventStorage()
        {
            
        }
        
        public void Dispose()
        {
            Clear();
        }
    }
}