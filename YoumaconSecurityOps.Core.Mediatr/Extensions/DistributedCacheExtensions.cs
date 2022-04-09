using System.IO;
using Microsoft.Extensions.Caching.Distributed;


namespace YoumaconSecurityOps.Core.Mediatr.Extensions
{
    /// <summary>
    /// <para>The <see cref="IDistributedCache"/> interface is designed to work solely with <see cref="Byte"/> <see cref="Array"/>s - unlike the <see cref="IMemoryCache"/> which can take any arbitrary type</para>
    /// <para>Microsoft has said "screw it we ain't doing this" when it comes to supporting the automatic serialization of objects</para>
    /// <para>This implementation was gratefully take from Stack Overflow by dzed at https://stackoverflow.com/a/50222288/2316834</para>
    /// <para>Thank you https://www.goodreads.com/book/show/29437996-copying-and-pasting-from-stack-overflow</para>
    /// </summary>
    public static class DistributedCacheExtensions
    {

        /// <summary>
        /// Stores the provided <typeparamref name="T" /> <paramref name="value"/> in the provided <paramref name="cache"/> with the given <paramref name="key"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">The cache we're writing to</param>
        /// <param name="key">The key we're using to pair with the value</param>
        /// <param name="value">The value we're attempting to cache</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task SetAsync<T>(this IDistributedCache cache, String key, T value, CancellationToken cancellationToken = default)
        {
            return SetAsync<T>(cache, key, value, new DistributedCacheEntryOptions(), cancellationToken);
        }

        /// <summary>
        /// Stores the provided <typeparamref name="T" /> <paramref name="value"/> in the provided <paramref name="cache"/> with the given <paramref name="key"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">The cache we're writing to</param>
        /// <param name="key">The key we're using to pair with the value</param>
        /// <param name="value">The value we're attempting to cache</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task SetAsync<T>(this IDistributedCache cache, String key, T value,
            DistributedCacheEntryOptions options, CancellationToken cancellationToken = default)
        {
            var  bytes = JsonSerializer.SerializeToUtf8Bytes(value,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return cache.SetAsync(key, bytes, options, cancellationToken);
        }

        /// <summary>
        /// Retrieves the <typeparamref name="T" /> value from the <paramref name="cache"/> specified by the <paramref name="key"/> asynchronously
        /// </summary>
        /// <typeparam name="T">The type of object we're trying to return from the cache</typeparam>
        /// <param name="cache">The distributed cache we're searching</param>
        /// <param name="key">The key to the <see cref="KeyValuePair"/> that holds the value we're looking for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, String key, CancellationToken cancellationToken = default)
        {
            var cachedValue = await cache.GetAsync(key, cancellationToken);

            if (cachedValue is null || !cachedValue.Any())
            {
                return default;
            }
            
            await using var stream = new MemoryStream(cachedValue);
            
            return await JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, cancellationToken);
        }

        /// <summary>
        /// Attempts to retrieve the <typeparam name="T"></typeparam> value specified by the provided <paramref name="key"/> for the <paramref name="cache"/> asynchronously
        /// </summary>
        /// <typeparam name="T">The type that we want to retrieve</typeparam>
        /// <param name="cache">The distributed cache we're searching</param>
        /// <param name="key">The key to the <see cref="KeyValuePair"/> that holds the value we're looking for</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="ValueTuple"/>(<c>False</c>, <typeparamref name="T"/> default) when value is not found,(<c>True</c>, <typeparamref name="T" /> value) when it is found</returns>
        public static async Task<(bool IsValueFound, T Value)> TryGetAsync<T>(this IDistributedCache cache, String key, CancellationToken cancellationToken = default)
        {
            var cachedValue = await cache.GetAsync(key, cancellationToken);

            if (cachedValue is null || !cachedValue.Any())
            {
                return (false, default);
            }

            await using var stream = new MemoryStream(cachedValue);

            var value = await JsonSerializer.DeserializeAsync<T>(stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, cancellationToken);

            return (true, value);
        }
    }
}
