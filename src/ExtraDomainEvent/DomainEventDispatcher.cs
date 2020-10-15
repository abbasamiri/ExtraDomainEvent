using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExtraDomainEvent.Abstractions;
using ExtraDomainEvent.Exceptions;
using ExtraDomainEvent.Helpers;
using ExtraDomainEvent.Policies;

namespace ExtraDomainEvent
{
    /// <summary>
    /// The default implementation of <see cref="IDomainEventDispatcher"/>.
    /// </summary>
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        /// <summary>
        /// The scanned domain-event-handlers. 
        /// </summary>
        protected readonly Dictionary<Type, Type> Handlers = new Dictionary<Type, Type>();

        /// <summary>
        /// The instance of <strong>EventStorage</strong>.
        /// </summary>
        protected readonly IEventStorage EventStorage;

        /// <summary>
        /// The constructor. 
        /// </summary>
        /// <param name="eventStorage"> An instance of <strong>EventStorage</strong> that implements <see cref="IEventStorage"/></param>
        /// <param name="assemblies">An array of assemblies that contain domain-event handlers.</param>
        /// <exception cref="DomainEventDispatcherException">The error occurs scanning assemblies, in the case of multiple handlers for a specific domain-event.</exception>
        public DomainEventDispatcher(IEventStorage eventStorage, IEnumerable<Assembly> assemblies)
        {
            Guard.ArgumentNotNull(eventStorage, nameof(eventStorage));
            Guard.ArgumentNotNull(assemblies, nameof(assemblies));
            EventStorage = eventStorage;

            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => p.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Any(i => i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<,>)))
                .ToList();


            if (!types.Any())
            {
                throw new DomainEventDispatcherException(
                    "The assemblies have no DomainEventHandler.");
            }

            foreach (var theType in types)
            {
                var eventType = theType.GetInterfaces()[0].GetGenericArguments()[0];
                if (Handlers.ContainsKey(eventType))
                {
                    throw new DomainEventDispatcherException(
                        $"An handler for the {eventType.FullName} has already been registered. A domain event can not have more than one handler.");
                }

                Handlers.Add(eventType, theType);
            }
        }

        /// <summary>
        /// Dispatches all events that are published and stored in event storage.
        /// </summary>
        /// <param name="context">An instance of domain-event-context that is passed to the handler.</param>
        /// <param name="dispatchPolicy">The dispatch policy.</param>
        /// <typeparam name="TDomainEventContext">The type of domain-event-context</typeparam>
        /// <returns>A task representing invoking all handlers.</returns>
        public async Task<DispatchResult[]> Dispatch<TDomainEventContext>(
            TDomainEventContext context,
            DispatchPolicy dispatchPolicy)
        {
            var result = new List<DispatchResult>();

            foreach (var eventType in EventStorage)
            {
                var dispatchResult = await Dispatch(eventType, context);
                result.Add(dispatchResult);
                if (dispatchPolicy == DispatchPolicy.TerminateOnFirstFailure)
                {
                    break;
                }
            }

            return await Task.FromResult(result.ToArray());
        }


        /// <summary>
        /// Dispatches an events that are published and stored in event storage.
        /// </summary>
        /// <param name="context">The <strong>DomainEventContext</strong> instance.</param>
        /// <typeparam name="TDomainEventContext">The DomainEventContext type.</typeparam>
        /// <returns>A task representing invoking the handler.</returns>
        protected async Task<DispatchResult> Dispatch<TDomainEventContext>(IDomainEvent @event,
            TDomainEventContext context)
        {
            DispatchResult result = null;
            var handlerType = Handlers[@event.GetType()];

            if (handlerType == null)
            {
                result = DispatchResult.Fail(
                    new DomainEventException($"There is no handler for {@event.GetType().FullName}", @event));
            }

            var instance = Activator.CreateInstance(handlerType!);

            var task = (Task) instance?.GetType().GetMethod("Handle")
                ?.Invoke(instance, new object[] {@event, context});

            if (task == null)
            {
                result = DispatchResult.Fail(
                    new DomainEventException($"Failed to call Handle method of {handlerType.FullName}", @event));
            }

            await task!.ConfigureAwait(false);

            var resultProperty = task.GetType().GetProperty("Result");
            if (resultProperty is { })
            {
                result = (DispatchResult) resultProperty.GetValue(task);
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Releases all resources.
        /// </summary>
        public void Dispose()
        {
            Handlers.Clear();
            EventStorage.Dispose();
        }
    }
}