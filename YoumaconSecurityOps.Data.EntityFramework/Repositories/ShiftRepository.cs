using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    internal sealed class ShiftRepository:  IShiftAccessor, IShiftRepository
    {
        private readonly ILogger<ShiftRepository> _logger;

        private readonly YoumaconSecurityDbContext _dbContext;

        public ShiftRepository(ILogger<ShiftRepository> logger,YoumaconSecurityDbContext dbContext)
        {
            _logger = logger;

            _dbContext = dbContext;
        }

        public IAsyncEnumerable<ShiftReader> GetAll(CancellationToken cancellationToken = new ())
        {
            var shifts = _dbContext.Shifts.AsAsyncEnumerable();

            return shifts;
        }

        public async Task<ShiftReader> WithId(Guid entityId, CancellationToken cancellationToken = new ())
        {
            var shift = await _dbContext.Shifts.AsQueryable()
                .SingleOrDefaultAsync(s => s.Id == entityId, cancellationToken);

            return shift;
        }

        public IAsyncEnumerator<ShiftReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
        {
            var getShiftAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

            return getShiftAsyncEnumerator;
        }

        public async Task<bool> AddAsync(ShiftReader entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Shifts.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ShiftReader> CheckIn(Guid shiftId, CancellationToken cancellationToken = default)
        {
            var shift = await WithId(shiftId, cancellationToken);

            var checkedInAt = DateTime.Now;

            shift.CheckedInAt = checkedInAt;

            shift.Notes += $"Checked In At: {checkedInAt:g}";

            _dbContext.Shifts.Update(shift);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return shift;
        }

        public async Task<ShiftReader> CheckOut(Guid shiftId, CancellationToken cancellationToken = default)
        {
            var shift = await WithId(shiftId, cancellationToken);

            var checkedOutAt = DateTime.Now;

            shift.CheckedOutAt = checkedOutAt;

            shift.Notes += $"Checked Out At {checkedOutAt:g}";

            _dbContext.Shifts.Update(shift);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return shift;
        }

        public async Task<ShiftReader> ReportIn(Guid shiftId, DateTime reportInAt, LocationReader currentLocation, CancellationToken cancellationToken = default)
        {
            var shiftToUpdate = await WithId(shiftId, cancellationToken);

            shiftToUpdate.LastReportedAt = reportInAt;

            if (shiftToUpdate.CurrentLocation != currentLocation)
            {
                shiftToUpdate.CurrentLocationId = currentLocation.Id;
            }

            shiftToUpdate.Notes += $"Reported In At: {reportInAt:g}";

            _dbContext.Shifts.Update(shiftToUpdate);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return shiftToUpdate;
        }
    }
}
