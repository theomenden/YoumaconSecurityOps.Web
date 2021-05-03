using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptionless;
using GenFu;
using Shouldly;
using Xunit;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Data.EntityFramework.Repositories;

namespace YoumaconSecurityOps.Data.EntityFramework.Tests.Repositories
{
    public class ContactRepositoryTests
    {
        private readonly YoumaconTestDbContext _testDbContext;

        private readonly IEnumerable<ContactReader> _contacts;

        private readonly ContactRepository _testRepository;

        public ContactRepositoryTests()
        {
            _contacts = new List<ContactReader>(75);

            _contacts = GenerateContacts();

            _testDbContext = new YoumaconTestDbContext();

            _testDbContext.Contacts.AddRange(_contacts);

            _testDbContext.SaveChanges();

            _testRepository = new ContactRepository(_testDbContext);

        }

        [Fact]
        public async Task GetAll_ShouldReturnAllContactsInDatabase()
        {
            //ARRANGE
            var countOfContacts = _contacts.Count();

            //ACT
            var result = await _testRepository.GetAll().ToListAsync();


            //ASSERT
            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeEmpty(),
                () => result.Count.ShouldBe(countOfContacts),
                () => result.ShouldContain(_contacts.Random())
                );
        }

        [Fact]
        public async Task WithId_ShouldReturnASingleContactThatMatchesSuppliedId()
        {
            //ARRANGE
            var contactToRetrieve = _contacts.Random();

            //ACT
            var result = await _testRepository.WithId(contactToRetrieve.Id);


            //ASSERT
            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.ShouldBe(contactToRetrieve)
                );
        }

        [Fact]
        public async Task AddAsync_ShouldAddANewContactToTheDataContext()
        {
            //ARRANGE
            var countOfContacts = _contacts.Count();

            var contactToAdd = A.New<ContactReader>();

            //ACT
            var result = await _testRepository.AddAsync(contactToAdd);

            //ASSERT
            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeTrue(),
                () => _testDbContext.Contacts.Count().ShouldBe(++countOfContacts)
                );
        }


        private static IEnumerable<ContactReader> GenerateContacts()
        {
            A.Configure<ContactReader>()
                .Fill(a => a.LastName).AsLastName()
                .Fill(b => b.FirstName).AsFirstName()
                .Fill(c => c.FacebookName).AsMusicArtistName()
                .Fill(d => d.PhoneNumber, RandomData.GetLong(1111111111,9999999999))
                .Fill(e => e.Email).AsEmailAddress();

            return A.ListOf<ContactReader>(RandomData.GetInt(10, 500));
        }
    }
}
