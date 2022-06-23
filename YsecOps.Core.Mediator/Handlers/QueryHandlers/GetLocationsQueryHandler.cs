using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YsecOps.Core.Mediator.Requests.Queries;
using YSecOps.Data.EfCore.Contexts;
using YSecOps.Data.EfCore.Models;

namespace YsecOps.Core.Mediator.Handlers.QueryHandlers;

internal sealed class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, List<Location>>
{
    private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

    public GetLocationsQueryHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public Task<List<Location>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var pronouns = context.Locations.OrderBy(l => l.Name);

        return pronouns.ToListAsync(cancellationToken);
    }
}
