using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    internal sealed class RadioScheduleRepository: IRadioScheduleAccessor, IRadioScheduleRepository
    {
        private readonly ILogger<RadioScheduleRepository> _logger;

        private readonly YoumaconSecurityDbContext _dbContext;

        public RadioScheduleRepository(ILogger<RadioScheduleRepository> logger, YoumaconSecurityDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IAsyncEnumerable<RadioScheduleReader> GetAll(CancellationToken cancellationToken = new ())
        {
            var radios = _dbContext.RadioSchedules
                .AsAsyncEnumerable()
                .OrderBy(r => r.RadioNumber);

            return radios;
        }

        public async Task<RadioScheduleReader> WithId(Guid entityId, CancellationToken cancellationToken = new ())
        {
            var radio = await _dbContext.RadioSchedules.AsQueryable().SingleOrDefaultAsync(r => r.Id == entityId, cancellationToken);

            return radio;
        }

        public IAsyncEnumerator<RadioScheduleReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
        {
            var radioScheduleAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

            return radioScheduleAsyncEnumerator;
        }

        public async Task<bool> AddAsync(RadioScheduleReader entity, CancellationToken cancellationToken = default)
        {
            var addResult = false;

            try
            {

                _dbContext.RadioSchedules.Add(entity);

                await _dbContext.SaveChangesAsync(cancellationToken);

                addResult = true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to add Radio: {@entity}, Reason {ex}", entity, ex.Message);
            }

            return addResult;

        }
    }
}
