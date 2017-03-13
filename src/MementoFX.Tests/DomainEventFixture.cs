using System;
using Memento;
using Xunit;

namespace MementoFX.Tests
{
    public class DomainEventFixture
    {
        [Fact]
        public void Ctor_should_set_Id_property()
        {
            var sut = new DummyEvent();
            Assert.Equal(Guid.Empty, sut.Id);
        }

        [Fact]
        public void Ctor_should_not_set_Timeline()
        {
            var sut = new DummyEvent();
            Assert.NotEqual(null, sut.TimelineId);
        }

        public class DummyEvent : DomainEvent
        {

        }
    }
}
