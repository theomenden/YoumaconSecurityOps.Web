using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace YsecOps.Core.Mediator.Handlers.QueryHandlers.StreamQueryHandlers;

internal class GetRadioScheduleQueryHandler: IStreamRequestHandler<GetAllRadiosQuery, RadioSchedule>
{
    private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

    public GetRadioScheduleQueryHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<RadioSchedule> Handle(GetAllRadiosQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var radios = context.RadioSchedules
            .Include(rs => rs.LastStaffToHave)
                .ThenInclude(ls => ls.Contacts)
                    .ThenInclude(c => c.Pronoun);

        await foreach (var radioSchedule in radios.AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            yield return radioSchedule;
        }
    }
}