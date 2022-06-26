using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YsecOps.Core.Mediator.Handlers.QueryHandlers
{
    internal sealed class GetCountOfStaffMembersQueryHandler : IRequestHandler<GetCountOfStaffMembersQuery, int>,
        IRequestHandler<GetStaffMembersOnBreakCountQuery, int>
    {
        private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

        public GetCountOfStaffMembersQueryHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> Handle(GetCountOfStaffMembersQuery request, CancellationToken cancellationToken)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            var staffCount = await context.Staff.CountAsync(cancellationToken);

            return staffCount;
        }

        public async Task<int> Handle(GetStaffMembersOnBreakCountQuery request, CancellationToken cancellationToken)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            var countMembersOnBreak = await context.Staff.CountAsync(member => member.IsOnBreak, cancellationToken);

            return countMembersOnBreak;
        }
    }
}
