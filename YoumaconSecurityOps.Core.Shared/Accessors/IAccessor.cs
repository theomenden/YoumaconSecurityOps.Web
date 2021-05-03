using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models;

namespace YoumaconSecurityOps.Core.Shared.Accessors
{
    public interface IAccessor<T>
    {
        IAsyncEnumerable<T> GetAll(CancellationToken cancellationToken = new ());

        Task<T> WithId(Guid entityId, CancellationToken cancellationToken = new());
    }
}
