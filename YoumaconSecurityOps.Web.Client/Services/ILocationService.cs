namespace YoumaconSecurityOps.Web.Client.Services;

public interface ILocationService
{
    Task<List<LocationReader>> GetLocationsAsync(GetLocationsQuery locationsQuery, CancellationToken cancellationToken = default);
}