using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenFu;
using Shouldly;
using Xunit;
using YoumaconSecurityOps.Core.EventStore.Events.Added;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Tests.EventTests
{
    public class EventBaseTests
    {
        [Fact]
        public void BaseEvent_ShouldBeCreatedWithJsonData()
        {
            //ARRANGE
            var testLocationReader = A.New<LocationReader>();

            var sut = new LocationAddedEvent(testLocationReader);


            //ACT //ASSERT
            sut.ShouldSatisfyAllConditions(
                () => sut.ShouldNotBeNull(),
                () => sut.LocationAdded.ShouldBe(testLocationReader),
                () => sut.DataAsJson.ShouldNotBeNullOrEmpty()
                );
        }
    }
}
