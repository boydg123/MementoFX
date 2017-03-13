using System;
using Memento.Domain;
using Xunit;
using Memento.Persistence;
using SharpTestsEx;
using Moq;
using Memento.Messaging;
//using Memento.Messaging;
//using Memento.Persistence.InMemory;

namespace Memento.Tests.Persistence
{
    public class EventStoreFixture
    {
        [Fact]
        public void ManageTimestamp_should_set_Timestamp_property()
        {
            var timestamp = new DateTime(2015, 11, 13);
            var @event = new FakeEventProvidingNaturalTimestamp()
            {
                EventProperty = 42,
                EventTimestamp = timestamp
            };
            EventStore.ManageTimestamp(@event);
            Assert.Equal(timestamp, @event.TimeStamp);
        }

        [Fact]
        public void ManageTimestamp_should_not_set_DateTime_properties_which_are_not_timestamps()
        {
            var timestamp = new DateTime(2015, 11, 13);
            var @event = new FakeEventProvidingNaturalTimestamp()
            {
                EventProperty = 42,
                EventTimestamp = timestamp
            };
            EventStore.ManageTimestamp(@event);
            Assert.NotEqual(timestamp, @event.AnotherDate);
        }

        [Fact]
        public void ManageTimestamp_should_not_alter_Timestamp_property()
        {
            var @event = new FakeEventUsingSurrogateTimestamp()
            {
                EventProperty = 42
            };
            var timestamp = @event.TimeStamp;
            EventStore.ManageTimestamp(@event);
            Assert.Equal(timestamp, @event.TimeStamp);
        }

        [Fact]
        public void ManageTimestamp_should_not_set_DateTime_properties_which_are_not_decorated_as_timestamps()
        {
            var timestamp = new DateTime(2015, 11, 13);
            var @event = new FakeEventUsingSurrogateTimestamp()
            {
                EventProperty = 42,
                EventTimestamp = timestamp
            };
            EventStore.ManageTimestamp(@event);
            Assert.NotEqual(timestamp, @event.AnotherDate);
        }

        //[Fact]
        //public void Save_should_throw_ArgumentNullException_on_null_parameter()
        //{
        //    var eventDispatcherMockBuilder = new Mock<IEventDispatcher>();

        //    var sut = new InMemoryEventStore(eventDispatcherMockBuilder.Object);
        //    Executing.This(() => sut.Save(null))
        //        .Should()
        //        .Throw<ArgumentNullException>()
        //        .And
        //        .ValueOf
        //        .ParamName
        //        .Should()
        //        .Be
        //        .EqualTo("event");
        //}


        public class FakeEventUsingSurrogateTimestamp : DomainEvent
        {
            public int EventProperty { get; set; }

            public DateTime EventTimestamp { get; set; }

            public DateTime AnotherDate { get; set; }
        }

        public class FakeEventProvidingNaturalTimestamp : DomainEvent
        {
            public int EventProperty { get; set; }

            [Timestamp]
            public DateTime EventTimestamp { get; set; }

            public DateTime AnotherDate { get; set; }
        }
    }
}
