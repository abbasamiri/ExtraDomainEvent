using System.Threading.Tasks;
using ExtraDomainEvent.Abstractions;
using ExtraDomainEvent.Exceptions;

namespace ExtraDomainEvent.Tests.System
{
    public class SecondDomainEventHandler : IDomainEventHandler<SecondDomainEvent, MainDomainEventContext>
    {
        public Task<DispatchResult> Handle(SecondDomainEvent domainEvent, MainDomainEventContext domainEventContext)
        {
            return Task.FromResult(DispatchResult.Fail(new DomainEventException(domainEvent)));
        }
    }
}