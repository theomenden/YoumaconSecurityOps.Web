using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Mediatr.Caching.Configurations.Request;
internal class GetPronounsQueryCache : CacheConfiguration<GetPronounsQuery, IEnumerable<Pronouns>>
{
    protected override CachingOptions<GetPronounsQuery> ConfigureCaching()
    {
        return new CachingOptions<GetPronounsQuery>
        {
            AbsoluteDuration = TimeSpan.FromHours(10),
            SlidingDuration = TimeSpan.FromHours(1)
        };
    }

    protected override Action<IServiceCollection>[] ConfigureCacheInvalidation()
    {
        return new[]
        {
          RegisterInvalidator<GetPronounsQuery, IEnumerable<Pronouns>>()
        };
    }
}

