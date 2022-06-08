

namespace YoumaconSecurityOps.Data.EntityFramework.Tests.Repositories;

public class StaffRepositoryTests
{
    private readonly YoumaconTestDbContext _testDbContext;

    private readonly IEnumerable<StaffReader> _staffMembers;

    private readonly StaffRepository _testRepository;

    public StaffRepositoryTests()
    {
        _staffMembers = new List<StaffReader>(50);

        _staffMembers = GenerateStaffMembers();

        _testDbContext = new YoumaconTestDbContext();

        _testDbContext.StaffMembers.AddRange(_staffMembers);

        _testDbContext.SaveChanges();

        _testRepository = new StaffRepository();
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllContactsInDatabase()
    {
        //ARRANGE
        var countOfContacts = _staffMembers.Count();

        //ACT
        var result = await _testRepository.GetAllAsync(_testDbContext).ToListAsync();


        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldNotBeEmpty(),
            () => result.Count.ShouldBe(countOfContacts),
            () => result.ShouldContain(_staffMembers.Random())
        );
    }

    [Fact]
    public async Task WithId_ShouldReturnASingleContactThatMatchesSuppliedId()
    {
        //ARRANGE
        var contactToRetrieve = _staffMembers.Random();

        //ACT
        var result = await _testRepository.WithIdAsync(_testDbContext, contactToRetrieve.Id);


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
        var countOfContacts = _staffMembers.Count();

        var staffMemberToAdd = A.New<StaffReader>();

        //ACT
        var result = await _testRepository.AddAsync(_testDbContext, staffMemberToAdd);

        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeTrue(),
            () => _testDbContext.StaffMembers.Count().ShouldBe(++countOfContacts)
        );
    }

    private static IEnumerable<StaffReader> GenerateStaffMembers()
    {
        var membersToGenerate = RandomData.GetInt(2,50);

        return A.ListOf<StaffReader>(membersToGenerate);
    }
}