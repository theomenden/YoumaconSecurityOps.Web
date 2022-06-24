using System.Runtime.CompilerServices;

namespace YsecOps.Core.Mediator.Handlers.QueryHandlers.StreamQueryHandlers;

internal sealed class GetStaffMembersQueryHandler : IStreamRequestHandler<GetStaffMembersQuery, Staff>
{
    private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

    public GetStaffMembersQueryHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<Staff> Handle(GetStaffMembersQuery request, [EnumeratorCancellation]CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var staff = context.Staff
            .Include(s => s.Contacts)
            .ThenInclude(c => c.Pronoun)
            .Include(s => s.StaffTypesRoles)
                .ThenInclude(sr => sr.Role)
            .Include(s => s.StaffTypesRoles)
                .ThenInclude(st => st.StaffType)
            .AsAsyncEnumerable();

        await foreach (var member in staff.WithCancellation(cancellationToken))
        {
            yield return member;
        }
    }
}