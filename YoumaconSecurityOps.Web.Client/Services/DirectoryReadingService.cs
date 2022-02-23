using System.Net.Http;
using System.Threading;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public class DirectoryReadingService: IDirectoryReadingService
    {
        private readonly HttpClient _client;

        public DirectoryReadingService(IHttpClientFactory client)
        {
            _client = client.CreateClient("imageClient");   
        }

        public IAsyncEnumerable<ImageInformationHolder> GetAllImagesAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
