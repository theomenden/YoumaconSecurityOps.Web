namespace YoumaconSecurityOps.Core.Mediatr.Commands;

/// <summary>
/// Command used to increase or decrease an incident's severity
/// </summary>
/// <remarks><see cref="Severity"/> given by <paramref name="UpdatedSeverityValue"/></remarks>
public record AdjustIncidentSeverityCommandWithReturn(Guid IncidentId,Severity UpdatedSeverityValue): ICommandWithReturn<Guid>
{

    public Guid Id => Guid.NewGuid();
}