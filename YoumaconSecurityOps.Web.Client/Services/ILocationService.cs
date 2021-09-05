using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public interface ILocationService
    {
        Task<List<LocationReader>> GetLocationsAsync(GetLocationsQuery locationsQuery, CancellationToken cancellationToken = default);
    }
}
