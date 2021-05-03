using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Repositories
{
    public interface IRepository<T>: IAsyncEnumerable<T>
    {
        Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default);
    }
}
