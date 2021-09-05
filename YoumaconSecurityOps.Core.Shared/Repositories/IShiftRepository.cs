using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Shared.Repositories
{
    /// <summary>
    /// For methods referring to the storage and updating of <see cref="ShiftReader"/> in the database
    /// </summary>
    /// <remarks>Implements <seealso cref="IRepository{T}"/></remarks>
    public interface IShiftRepository: IRepository<ShiftReader>
    {
        /// <summary>
        /// Updates a particular shift's Check In date, and notes it in the database.
        /// </summary>
        /// <param name="shiftId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated <see cref="ShiftReader"/></returns>
        Task<ShiftReader> CheckIn(Guid shiftId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a particular shift's Check Out date, and notes it in the database
        /// </summary>
        /// <param name="shiftId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated <see cref="ShiftReader"/></returns>
        Task<ShiftReader> CheckOut(Guid shiftId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a particular shift's <paramref name="currentLocationId"/> that the shift is reporting from.
        /// </summary>
        /// <param name="shiftId"></param>
        /// <param name="currentLocationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated <see cref="ShiftReader"/></returns>
        Task<ShiftReader> ReportIn(Guid shiftId, Guid currentLocationId, CancellationToken cancellationToken = default);
    }
}
