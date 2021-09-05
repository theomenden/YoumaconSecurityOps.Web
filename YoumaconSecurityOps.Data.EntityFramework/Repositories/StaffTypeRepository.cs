using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Data.EntityFramework.Context;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    internal sealed class StaffTypeRepository: IStaffTypeAccessor
    {
        private readonly YoumaconSecurityDbContext _dbContext;

        private readonly ILogger<StaffTypeRepository> _logger;

        public StaffTypeRepository(YoumaconSecurityDbContext dbContext, ILogger<StaffTypeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;   
        }

        public IAsyncEnumerable<StaffType> GetAll(CancellationToken cancellationToken = new CancellationToken())
        {
            var staffTypes = _dbContext.StaffTypes.AsAsyncEnumerable()
                .OrderBy(t => t.Id);

            return staffTypes;
        }

        public async Task<StaffType> WithId(Int32 staffTypeId, CancellationToken cancellationToken = new CancellationToken())
        {
            var typeToFind = await _dbContext.StaffTypes.SingleOrDefaultAsync(st => st.Id == staffTypeId, cancellationToken);

            return typeToFind;
        }

        public IAsyncEnumerator<StaffType> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            var staffTypeEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

            return staffTypeEnumerator;
        }
    }
}
