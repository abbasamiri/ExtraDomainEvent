using ExtraDomainEvent.Exceptions;
using ExtraDomainEvent.Policies;
using ExtraDomainEvent.Tests.System;
using Xunit;

namespace ExtraDomainEvent.Tests
{
    public class DomainEventDispatcherTest
    {
        [Fact]
        public void Constructor_Throws_DomainEventDispatcherException_If_The_Assemblies_Have_No_DomainEventHandler()
        {
            Assert.Throws<DomainEventDispatcherException>(() =>
            {
                var eventStorage = new EventStorage();
                var domainEventDispatcher = new DomainEventDispatcher(
                    eventStorage,
                    new[] {typeof(Empty.Empty).Assembly});
            });
        }

        [Fact]
        public void Constructor_Throws_DomainEventDispatcherException_If_A_Domain_Event_Has_More_Than_One_Handler()
        {
            var eventStorage = new EventStorage();
            Assert.Throws<DomainEventDispatcherException>(() =>
            {
                var domainEventDispatcher = new DomainEventDispatcher(
                    eventStorage,
                    new[] {typeof(DomainEventOne).Assembly});
            });
        }

        [Fact]
        public async void Dispatch_Returns_Success_If_The_Handler_Returns_Success()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);
            var domainEventDispatcher = new DomainEventDispatcher(
                eventStorage,
                new[] {this.GetType().Assembly});

            var domainEventContext = new MainDomainEventContext();

            domainEventPublisher.Publish(new FirstDomainEvent());
            var result =
                await domainEventDispatcher.Dispatch(domainEventContext, DispatchPolicy.IgnoreFailureAndContinue);
            Assert.True(result[0].Success);
        }

        [Fact]
        public async void Dispatch_Returns_Failure_If_The_Handler_Returns_Failure()
        {
            var eventStorage = new EventStorage();
            var domainEventPublisher = new DomainEventPublisher(eventStorage, PublishPolicy.None);
            var domainEventDispatcher = new DomainEventDispatcher(
                eventStorage,
                new[] {this.GetType().Assembly});

            var domainEventContext = new MainDomainEventContext();

            domainEventPublisher.Publish(new SecondDomainEvent());
            var result =
                await domainEventDispatcher.Dispatch(domainEventContext, DispatchPolicy.IgnoreFailureAndContinue);
            Assert.True(result[0].Failure);
        }
    }
}