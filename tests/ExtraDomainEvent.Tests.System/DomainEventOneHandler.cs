using System.Threading.Tasks;
using ExtraDomainEvent.Abstractions;

namespace ExtraDomainEvent.Tests.System
{
    public class DomainEventOneHandler : IDomainEventHandler<DomainEventOne,DomainEventContext>
    {
        public Task<DispatchResult> Handle(DomainEventOne domainEvent, DomainEventContext domainEventContext)
        {
            throw new global::System.NotImplementedException();
        }
    }
}