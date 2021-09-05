using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Data.EntityFramework.Context;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    internal class StaffRoleRepository : IStaffRoleAccessor
    {
        private readonly YoumaconSecurityDbContext _dbContext;

        private readonly ILogger<StaffRoleRepository> _logger;

        public StaffRoleRepository(YoumaconSecurityDbContext dbContext, ILogger<StaffRoleRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException("Could Not be injected", nameof(dbContext));

            _logger = logger ?? throw new ArgumentException("Could Not be injected", nameof(logger));
        }


        public IAsyncEnumerable<StaffRole> GetAll(CancellationToken cancellationToken = default)
        {
            var staffRoles = _dbContext.StaffRoles.AsAsyncEnumerable()
                    .OrderBy(sr => sr.Id);
            
            return staffRoles;
        }
        public Task<StaffRole> WithId(int staffRoleId)
        {
            var role =  _dbContext.StaffRoles.SingleOrDefault(r => r.Id == staffRoleId);

            return Task.FromResult(role);
        }

        public IAsyncEnumerator<StaffRole> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);
        }
    }
}
