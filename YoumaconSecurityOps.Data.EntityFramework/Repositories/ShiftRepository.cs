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
using YoumaconSecurityOps.Data.EntityFramework.Context;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>DON'T FORGET TO DEFINE THE GOD DAMN METHODS - Emma 8/27/2021 - 3:05am</remarks>
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
            entity.CurrentLocationId = entity.StartingLocationId;

            bool addResult;

            try
            {
                await _dbContext.Shifts.AddAsync(entity, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                addResult = true;
            }
            catch (Exception e)
            {
                _logger.LogError("Task<bool> AddAsync(ShiftReader entity, CancellationToken cancellationToken = default) threw an exception: {e}", e.Message);

                addResult = false;
            }

            return addResult;
        }

        #region Shift Update Methods
        public async Task<ShiftReader> CheckIn(Guid shiftId, CancellationToken cancellationToken = default)
        {
            var shift = await _dbContext.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

            var checkedInAt = DateTime.Now;

            shift.CheckedInAt = checkedInAt;

            shift.Notes += $"Checked In At: {checkedInAt:g}";

            await _dbContext.SaveChangesAsync(cancellationToken);

            return shift;
        }

        public async Task<ShiftReader> CheckOut(Guid shiftId, CancellationToken cancellationToken = default)
        {
            var shift = await _dbContext.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

            var checkedOutAt = DateTime.Now;

            shift.CheckedOutAt = checkedOutAt;

            shift.Notes += $"Checked Out At {checkedOutAt:g}";

            await _dbContext.SaveChangesAsync(cancellationToken);

            return shift;
        }

        public async Task<ShiftReader> ReportIn(Guid shiftId, Guid currentLocationId, CancellationToken cancellationToken = default)
        {
            var shiftToUpdate = await _dbContext.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

            var reportedInAt = DateTime.Now;

            shiftToUpdate.LastReportedAt = reportedInAt;

            shiftToUpdate.CurrentLocationId = currentLocationId;

            shiftToUpdate.Notes += $"Reported In At: {reportedInAt:g}";

            await _dbContext.SaveChangesAsync(cancellationToken);

            return shiftToUpdate;
        }


        #endregion
    }
}
