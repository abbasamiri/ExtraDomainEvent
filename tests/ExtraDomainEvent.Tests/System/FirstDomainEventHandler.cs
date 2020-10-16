using System.Threading.Tasks;
using ExtraDomainEvent.Abstractions;

namespace ExtraDomainEvent.Tests.System
{
    public class FirstDomainEventHandler : IDomainEventHandler<FirstDomainEvent, MainDomainEventContext>
    {
        public Task<DispatchResult> Handle(FirstDomainEvent domainEvent, MainDomainEventContext domainEventContext)
        {
            return Task.FromResult(DispatchResult.Ok());
        }
    }
}