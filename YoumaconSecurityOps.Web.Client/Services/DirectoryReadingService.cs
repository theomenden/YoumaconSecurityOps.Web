using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Web.Client.Models;

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
