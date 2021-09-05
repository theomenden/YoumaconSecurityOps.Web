using System;
using YoumaconSecurityOps.Core.Shared.Enumerations;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    /// <summary>
    /// Command used to increase or decrease an incident's severity
    /// </summary>
    /// <remarks><see cref="Severity"/> given by <paramref name="UpdatedSeverityValue"/></remarks>
    public record AdjustIncidentSeverityCommand(Guid IncidentId,Severity UpdatedSeverityValue): ICommand<Guid>
    {

        public Guid Id => Guid.NewGuid();
    }
}
