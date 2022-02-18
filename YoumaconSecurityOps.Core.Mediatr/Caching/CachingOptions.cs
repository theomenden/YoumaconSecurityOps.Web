namespace YoumaconSecurityOps.Core.Mediatr.Caching;

/// <summary>
/// Container for options with regards to caching types of <typeparamref name="TCache"/>
/// </summary>
/// <typeparam name="TCache"></typeparam>
public class CachingOptions<TCache>
{
    /// <value>
    /// The maximum duration for which a cache can exist
    /// </value>
    public TimeSpan? AbsoluteDuration { get; set; }

    /// <value>
    /// A window of time a cache can exist
    /// </value>
    /// <remarks>Cannot exceed the <see cref="AbsoluteDuration"/> value</remarks>
    public TimeSpan? SlidingDuration { get; set; }

    /// <value>
    /// An optional prefix for a key to the Cache
    /// </value>
    /// <remarks>Defaults to the Query Fullname</remarks>
    public string CachePrefix { get; set; }

    /// <value>
    /// A function that generates the key for the cache
    /// </value>
    /// <remarks>Defaults to the Query Fullname</remarks>
    public Func<TCache, string> KeyGenerator { get; set; }
}