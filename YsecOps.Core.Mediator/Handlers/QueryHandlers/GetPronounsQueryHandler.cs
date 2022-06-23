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

internal sealed class GetPronounsQueryHandler : IRequestHandler<GetPronounsQuery, List<Pronoun>>
{
    private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

    public GetPronounsQueryHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public Task<List<Pronoun>> Handle(GetPronounsQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var pronouns = context.Pronouns.OrderBy(p => p.Id);

        return pronouns.ToListAsync(cancellationToken);
    }
}