using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Accessors
{
    public interface IAccessor<T>
    {
        IAsyncEnumerable<T> GetAll(CancellationToken cancellationToken = new ());

        Task<T> WithId(Guid entityId, CancellationToken cancellationToken = new());
    }
}
