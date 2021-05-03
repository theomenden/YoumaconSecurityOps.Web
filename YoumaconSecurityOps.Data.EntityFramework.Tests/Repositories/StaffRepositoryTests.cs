using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptionless;
using GenFu;
using Xunit;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Data.EntityFramework.Repositories;

namespace YoumaconSecurityOps.Data.EntityFramework.Tests.Repositories
{
    public class StaffRepositoryTests
    {
        private readonly YoumaconTestDbContext _testDbContext;

        private readonly IEnumerable<StaffReader> _staffMembers;

        private readonly StaffRepository _staff;

        public StaffRepositoryTests()
        {
            _testDbContext = new YoumaconTestDbContext();

            _staffMembers = new List<StaffReader>(50);

            _staffMembers = GenerateStaffMembers();
        }

        private static IEnumerable<StaffReader> GenerateStaffMembers()
        {
            var membersToGenerate = RandomData.GetInt(2,50);

            A.Configure<ContactReader>()
                .Fill(a => a.LastName).AsLastName()
                .Fill(b => b.FirstName).AsFirstName()
                .Fill(c => c.FacebookName).AsMusicArtistName()
                .Fill(d => d.PhoneNumber, RandomData.GetLong(1111111111, 9999999999))
                .Fill(e => e.Email).AsEmailAddress();

            var contactInformation = A.ListOf<ContactReader>(membersToGenerate);

            A.Configure<StaffReader>()
                .Fill(a => a.ContactId, contactInformation.Random().Id);


            return A.ListOf<StaffReader>(membersToGenerate);
        }
    }
}
