using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Web.Client.Exceptions;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public sealed class ApiService: IApiService
    {
        private readonly HttpClient _client;

        public ApiService(HttpClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
        }

        public async Task<T> GetContentAsync<T>(String uri)
        {
            var responseContent = await _client.GetFromJsonAsync<T>(uri);

            return responseContent;
        }

        public async Task<List<T>> GetContentStreamAsync<T>(String uri, CancellationToken cancellationToken = new ())
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{_client.BaseAddress}{uri}");
            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await DeserializeFromStream<List<T>>(stream, cancellationToken);
            }

            var content = await DeserializeStreamToStringAsync(stream);

            throw new ApiException
            {
                StatusCode = (int)response.StatusCode,
                Content = content
            };

        }

        public async Task<HttpResponseMessage> PostContent<T>(String uri, T body, CancellationToken cancellationToken = new CancellationToken())
        {
            var responseContent = JsonSerializer.Serialize(body);

            using var httpResponseMessage = await _client.PostAsync(new Uri(uri), new StringContent(responseContent, Encoding.UTF8, MediaTypeNames.Application.Json), cancellationToken);

            return httpResponseMessage;
        }

        private static async Task<T> DeserializeFromStream<T>(Stream stream, CancellationToken cancellationToken)
        {
            if (stream is null || stream.CanRead is false)
            {
                return default;
            }

            var searchResult = await JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);

            return searchResult;
        }

        private static async Task<String> DeserializeStreamToStringAsync(Stream stream)
        {
            var content = String.Empty;

            if (stream is null)
            {
                return content;
            }

            using var sr = new StreamReader(stream);

            content = await sr.ReadToEndAsync();

            return content;
        }
    }
}
