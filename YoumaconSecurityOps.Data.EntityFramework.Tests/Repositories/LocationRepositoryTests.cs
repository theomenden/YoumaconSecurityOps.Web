namespace YoumaconSecurityOps.Data.EntityFramework.Tests.Repositories;

public class LocationRepositoryTests
{
    private readonly YoumaconTestDbContext _testDbContext;

    private readonly LocationRepository _locationRepository;

    private readonly IEnumerable<LocationReader> _locations;
    
    public LocationRepositoryTests()
    {
        _locations = new List<LocationReader>(50);

        _locations = GenerateLocations();

        _testDbContext = new YoumaconTestDbContext();

        _testDbContext.Locations.AddRange(_locations);

        _testDbContext.SaveChanges();

        _locationRepository = new LocationRepository();
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllLocationsInDatabase()
    {
        //ARRANGE
        var countOfLocations = _locations.Count();

        //ACT
        var results = await _locationRepository.GetAllAsync(_testDbContext).ToListAsync();

        //ASSERT
        results.ShouldSatisfyAllConditions(
            () => results.ShouldNotBeEmpty(),
            () => results.Count.ShouldBe(countOfLocations),
            () => results.ShouldContain(_locations.Random())
        );
    }

    [Fact]
    public async Task WithId_ShouldReturnSpecificLocationUsingSuppliedId()
    {
        //ARRANGE
        var location = _locations.Random();

        //ACT
        var result = await _locationRepository.WithIdAsync(_testDbContext, location.Id);

        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldNotBeNull(),
            () => result.ShouldBe(location)
        );
    }
    
    [Fact]
    public async Task Add_ShouldAddANewLocationToTheContext()
    {
        //ARRANGE
        var locationToAdd = A.New<LocationReader>();

        var locationInitialCount = _locations.Count();

        //ACT
        var result = await _locationRepository.AddAsync(_testDbContext, locationToAdd);

        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeTrue(),
            () => _testDbContext.Locations.Count().ShouldBe(++locationInitialCount)
        );
    }

    private static IEnumerable<LocationReader> GenerateLocations()
    {

        A.Configure<LocationReader>()
            .Fill(a => a.Name).AsCity()
            .Fill(b => b.IsHotel, RandomData.GetBool(80));

        return A.ListOf<LocationReader>(RandomData.GetInt(10, 30));
    }

}