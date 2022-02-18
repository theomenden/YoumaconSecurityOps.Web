namespace YoumaconSecurityOps.Core.Shared.Accessors;

public interface ILocationAccessor: IAccessor<LocationReader>
{
    IAsyncEnumerable<LocationReader> GetHotels(CancellationToken cancellationToken = new());
}