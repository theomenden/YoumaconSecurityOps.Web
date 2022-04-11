namespace YoumaconSecurityOps.Web.Client.Services;

public class LocationService: ILocationService
{
    private readonly IMediator _mediator;

    private readonly ILogger<LocationService> _logger;

    private readonly LocationsIndexedDbRepository _locationsIndexedDbRepository;

    private readonly YSecServiceOptions _configuration;

    public LocationService(IMediator mediator, ILogger<LocationService> logger, LocationsIndexedDbRepository locationsIndexedDbRepository, YSecServiceOptions configuration)
    {
        _mediator = mediator;
        _logger = logger;
        _locationsIndexedDbRepository = locationsIndexedDbRepository;
        _configuration = configuration;
    }

    public async Task<List<LocationReader>> GetLocationsAsync(GetLocationsQuery locationsQuery, CancellationToken cancellationToken = default)
    {
        if (await IsLocationsStoreInvalidatedAsync(cancellationToken))
        {
            await PurgeInvalidStoreAsync(cancellationToken);

            var locations = await _mediator.Send(locationsQuery, cancellationToken);

            await _locationsIndexedDbRepository.CreateOrUpdateMultipleAsync(locations.ToArray(), cancellationToken);
        }

        return await _locationsIndexedDbRepository.GetAllAsync(cancellationToken);
    }

    private async Task<Boolean> IsLocationsStoreInvalidatedAsync(CancellationToken cancellationToken = default)
    {
        var isStoreEmpty = await _locationsIndexedDbRepository.IsEmptyAsync(cancellationToken);

        var currentTime = DateTime.Now;

        var isSlidingWindowExpired = currentTime > _configuration.GetSlidingWindowRelativeToNow() 
            || currentTime >= _configuration.GetAbsoluteWindowRelativeToNow();

        return isStoreEmpty || isSlidingWindowExpired;
    }

    private async Task PurgeInvalidStoreAsync(CancellationToken cancellationToken = default)
    {
        await _locationsIndexedDbRepository.ClearAsync(cancellationToken);
    }
}