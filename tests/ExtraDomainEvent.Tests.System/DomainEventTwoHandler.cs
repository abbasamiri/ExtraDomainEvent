﻿using System.Threading.Tasks;
using ExtraDomainEvent.Abstractions;

namespace ExtraDomainEvent.Tests.System
{
    public class DomainEventTwoHandler : IDomainEventHandler<DomainEventTwo,DomainEventContext>
    {
        public Task<DispatchResult> Handle(DomainEventTwo domainEvent, DomainEventContext domainEventContext)
        {
            throw new global::System.NotImplementedException();
        }
    }
}