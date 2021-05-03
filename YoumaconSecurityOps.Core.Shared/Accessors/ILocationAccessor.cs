using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Shared.Accessors
{
    public interface ILocationAccessor: IAccessor<LocationReader>
    {
        IAsyncEnumerable<LocationReader> GetHotels(CancellationToken cancellationToken = new());
    }
}
