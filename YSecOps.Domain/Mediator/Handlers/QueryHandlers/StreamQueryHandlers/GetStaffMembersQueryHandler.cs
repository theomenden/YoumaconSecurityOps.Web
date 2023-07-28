using System.Runtime.CompilerServices;

namespace YsecOps.Core.Mediator.Handlers.QueryHandlers.StreamQueryHandlers;

internal sealed class GetStaffMembersQueryHandler : IStreamRequestHandler<GetStaffMembersQuery, Staff>,
    IStreamRequestHandler<GetStaffMembersWithParametersQuery, Staff>
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

    public async IAsyncEnumerable<Staff> Handle(GetStaffMembersWithParametersQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var searchParameters = request.Parameters;

        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var staff = context.Staff
            .Include(s => s.Contacts)
                .ThenInclude(c => c.Pronoun)
            .Include(s => s.StaffTypesRoles)
                .ThenInclude(sr => sr.Role)
            .Include(s => s.StaffTypesRoles)
                .ThenInclude(st => st.StaffType)
            .Where(member => member.IsBlackShirt == searchParameters.IsBlackShirt)
            .Where(member => member.NeedsCrashSpace == searchParameters.NeedsCrashSpace)
            .Where(member => member.IsRaveApproved == searchParameters.IsRaveApproved);

        if (searchParameters.StaffIds.Any())
        {
            staff = staff.Where(member => searchParameters.StaffIds.Contains(member.Id));
        }


        await foreach (var member in staff.AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            yield return member;
        }
    }
}