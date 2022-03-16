using System.Threading;

namespace YoumaconSecurityOps.Web.Client.Services;

public interface IDirectoryReadingService
{
    IAsyncEnumerable<ImageInformationHolder> GetAllImagesAsync(CancellationToken cancellationToken);
}