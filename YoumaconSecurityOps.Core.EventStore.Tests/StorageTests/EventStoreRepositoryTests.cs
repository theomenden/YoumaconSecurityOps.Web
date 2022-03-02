using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Exceptionless;
using GenFu;
using Shouldly;
using Xunit;
using YoumaconSecurityOps.Core.EventStore.Events;
using YoumaconSecurityOps.Core.EventStore.Events.Added;
using YoumaconSecurityOps.Core.EventStore.Events.Created;
using YoumaconSecurityOps.Core.EventStore.Storage;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Tests.StorageTests
{
    public class EventStoreRepositoryTests
    {
        private readonly YSecTestEventStoreContext _testDbContext;

        private readonly IEnumerable<EventReader> _events;

        private readonly EventStoreRepository _eventStoreRepository;

        public EventStoreRepositoryTests()
        {
            _testDbContext = new YSecTestEventStoreContext();

            _events = GenerateEvents();

            _testDbContext.Events.AddRange(_events);

            _testDbContext.SaveChanges();

            //_eventStoreRepository = new EventStoreRepository(_testDbContext);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEventsInTheStore()
        {
            //ARRANGE
            var eventsCount = _events.Count();

            //ACT
            var results = await _eventStoreRepository.GetAll(_testDbContext).ToListAsync();

            //ASSERT
            results.ShouldSatisfyAllConditions(
                () => results.Count.ShouldBe(eventsCount),
                () => results.ShouldNotBeEmpty(),
                () => results.ShouldContain(_events.Random())
                );
        }

        [Fact]
        public async Task GetAllByAggregateId_ShouldReturnAllEventsInTheStore()
        {
            //ARRANGE


            var aggregateId = _events.Random().Id;

            var eventsCount = _events.Count(a => a.Id == aggregateId);

            //ACT
            var results = await _eventStoreRepository.GetAllByAggregateId(_testDbContext,aggregateId).ToListAsync();

            //ASSERT
            results.ShouldSatisfyAllConditions(
                () => results.Count.ShouldBe(eventsCount),
                () => results.ShouldNotBeEmpty(),
                () => results.ShouldAllBe(e => e.Id.Equals(aggregateId))
            );
        }

        private static IEnumerable<EventReader> GenerateEvents()
        {
            A.Configure<EventReader>()
                .Fill(a => a.Name).AsLoremIpsumSentences()
                .Fill(b => b.Id, Guid.NewGuid())
                .Fill(c => c.Aggregate).AsMusicGenreName()
                .Fill(d => d.Data).AsLoremIpsumSentences(4)
                .Fill(e => e.MinorVersion).WithinRange(1,10000);

            var eventReaders =  A.ListOf<EventReader>(RandomData.GetInt(1,500));

            return eventReaders;
        }
    }
}
