using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public interface IApiService
    {
        Task<T> GetContentAsync<T>(String uri);

        Task<List<T>> GetContentStreamAsync<T>(String uri, CancellationToken cancellationToken = new ());

        Task<HttpResponseMessage> PostContent<T>(String uri, T body,
            CancellationToken cancellationToken = new ());
    }
}
