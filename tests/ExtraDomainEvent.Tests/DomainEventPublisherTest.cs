using System;
using ExtraDomainEvent.Abstractions;
using ExtraDomainEvent.Exceptions;
using ExtraDomainEvent.Policies;
using ExtraDomainEvent.Tests.System;
using Xunit;

namespace ExtraDomainEvent.Tests
{
    public class DomainEventPublisherTest
    {
        [Fact]
        public void Publish_Saves_Event_Parameter_If_It_Is_Not_Null()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);

            domainEventPublisher.Publish(new FirstDomainEvent());
            Assert.Single(eventStorage);
        }

        [Fact]
        public void Publish_Throws_DomainEventPublisherException_If_The_DomainEvent_Type_Has_Been_Published_Before()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.PreventDuplicateType);
            Assert.Throws<DomainEventPublisherException>(() =>
            {
                domainEventPublisher.Publish(new IDomainEvent[]
                {
                    new FirstDomainEvent(),
                    new FirstDomainEvent()
                });
            });
        }

        [Fact]
        public void Publish_Throws_DomainEventPublisherException_If_The_DomainEvent_Reference_Has_Been_Published_Before()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.PreventDuplicateReference);
            var domainEventOne = new FirstDomainEvent();
            Assert.Throws<DomainEventPublisherException>(() =>
            {
                domainEventPublisher.Publish(new IDomainEvent[]
                {
                    domainEventOne,
                    domainEventOne
                });
            });
        }

        [Fact]
        public void Publish_Throws_ArgumentNullException_If_The_Parameter_Is_Null()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);
            Assert.Throws<ArgumentNullException>(() => { domainEventPublisher.Publish((IDomainEvent) null); });
        }

        [Fact]
        public void Publish_Saves_Events_Parameter_If_It_Is_Not_Null()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);
            domainEventPublisher.Publish(new IDomainEvent[]
            {
                new FirstDomainEvent(),
                new SecondDomainEvent()
            });
            Assert.Equal(2, eventStorage.Count);
        }

        [Fact]
        public void Publish_Throws_ArgumentNullException_If_Any_Member_In_The_List_Is_Null()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);

            Assert.Throws<ArgumentNullException>(() =>
            {
                domainEventPublisher.Publish(new IDomainEvent[]
                {
                    new SecondDomainEvent(), 
                    null
                });
            });
        }

        [Fact]
        public void ClearEvents_Removes_All_Events_In_The_EventStorage()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);

            domainEventPublisher.Publish(new IDomainEvent[]
            {
                new FirstDomainEvent(),
                new SecondDomainEvent()
            });
            domainEventPublisher.ClearEvents();
            Assert.Empty(eventStorage);
        }

        [Fact]
        public void RemoveEvent_Removes_An_Event_In_The_EventStorage()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);

            var domainEventOne = new FirstDomainEvent();
            var domainEventTwo = new SecondDomainEvent();

            domainEventPublisher.Publish(new IDomainEvent[]
            {
                domainEventTwo,
                domainEventOne
            });
            domainEventPublisher.RemoveEvent(domainEventOne);
            Assert.DoesNotContain(domainEventOne, eventStorage);
        }

        [Fact]
        public void GetEvents_Returns_All_Elements_In_The_EventStorage()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);

            var domainEventOne = new FirstDomainEvent();
            var domainEventTwo = new SecondDomainEvent();

            domainEventPublisher.Publish(new IDomainEvent[]
            {
                domainEventTwo,
                domainEventOne
            });
            Assert.Equal(2, eventStorage.Count);
        }
    }
}