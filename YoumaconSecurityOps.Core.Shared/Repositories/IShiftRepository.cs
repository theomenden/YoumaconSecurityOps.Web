using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Shared.Repositories
{
    public interface IShiftRepository: IRepository<ShiftReader>
    {
        Task<ShiftReader> CheckIn(Guid shiftId, CancellationToken cancellationToken = default);

        Task<ShiftReader> CheckOut(Guid shiftId, CancellationToken cancellationToken = default);

        Task<ShiftReader> ReportIn(Guid shiftId, DateTime reportInAt, LocationReader currentLocation, CancellationToken cancellationToken = default);
    }
}
