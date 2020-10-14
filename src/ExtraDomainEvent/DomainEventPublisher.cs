using System;
using System.Collections.Generic;
using System.Linq;
using ExtraDomainEvent.Abstractions;
using ExtraDomainEvent.Exceptions;
using ExtraDomainEvent.Helpers;
using ExtraDomainEvent.Policies;

namespace ExtraDomainEvent
{
    /// <summary>
    /// The default implementation of <see cref="IDomainEventPublisher"/>
    /// </summary>
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IEventStorage _eventStorage;
        private readonly PublishPolicy _policy;

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="eventStorage">The instance of EventStorage</param>
        /// <param name="policy"></param>
        public DomainEventPublisher(IEventStorage eventStorage, PublishPolicy policy)
        {
            Guard.ArgumentNotNull(eventStorage, nameof(eventStorage));
            _eventStorage = eventStorage;
            _policy = policy;
        }

        /// <summary>
        /// Stores a domain-event into the storage.
        /// </summary>
        /// <param name="event">A domain-events to store.</param>
        public void Publish(IDomainEvent @event)
        {
            Guard.ArgumentNotNull(@event, nameof(@event));

            switch (_policy)
            {
                case PublishPolicy.PreventDuplicateType:
                    if (_eventStorage.Any(c => c.GetType() == @event.GetType()))
                    {
                        throw new DomainEventPublisherException(
                            $"An event with the type {@event.GetType().Name} has already been published.");
                    }
                    break;
                case PublishPolicy.None:
                    break;
                case PublishPolicy.PreventDuplicateReference:
                    if (_eventStorage.Contains(@event))
                    {
                        throw new DomainEventPublisherException(
                            $"The event has already been published");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _eventStorage.Add(@event);
        }

        /// <summary>
        /// Stores multiple domain-events into the storage. 
        /// </summary>
        /// <param name="events">The list of domain-events to store.</param>
        public void Publish(IEnumerable<IDomainEvent> events)
        {
            Guard.ArgumentNotNull(events, nameof(events));

            foreach (var @event in events)
            {
                Publish(@event);
            }
        }

        /// <summary>
        /// Remove all published domain-event from the storage.
        /// </summary>
        public void ClearEvents()
        {
            _eventStorage.Clear();
        }
        
        /// <summary>
        /// Removes a published domain-event from the storage.
        /// </summary>
        /// <param name="event">The domain-event.</param>
        /// <returns></returns>
        public bool RemoveEvent(IDomainEvent @event)
        {
            return _eventStorage.Remove(@event);
        }

        /// <summary>
        /// Gets all published domain-events.
        /// </summary>
        /// <returns>The list of all published domain-events.</returns>
        public IEnumerable<IDomainEvent> GetEvents()
        {
            return _eventStorage;
        }

        /// <summary>
        /// Releases all resources.
        /// </summary>
        public void Dispose()
        {
            _eventStorage.Dispose();
        }
    }
}