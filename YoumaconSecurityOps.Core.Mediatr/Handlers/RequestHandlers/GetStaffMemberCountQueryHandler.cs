using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;
public class GetStaffMemberCountQueryHandler : IRequestHandler<GetStaffMemberCount, Int32>, IRequestHandler<GetFilteredStaffCount, Int32>
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;


    public GetStaffMemberCountQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<int> Handle(GetStaffMemberCount request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var totalStaffMembers = await context.StaffMembers.AsQueryable().CountAsync(cancellationToken);

        return totalStaffMembers;
    }

    public async Task<int> Handle(GetFilteredStaffCount request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var totalStaffMembersAfterFilter = await context.StaffMembers
            .Where(sr => sr.IsOnBreak == request.IsOnBreak)
            .Where(sr => sr.IsRaveApproved == request.IsRaveApproved)
            .Where(sr => sr.IsBlackShirt == request.IsBlackShirt)
            .Where(sr => sr.NeedsCrashSpace == request.NeedsCrashSpace)
            .AsQueryable()
            .CountAsync(cancellationToken);

        return totalStaffMembersAfterFilter;
    }
}
