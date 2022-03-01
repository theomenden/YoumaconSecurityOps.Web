namespace YoumaconSecurityOps.Core.Mediatr.Commands;

/// <summary>
/// Command to resolve a particular incident given by its <paramref name="IncidentId"/>
/// </summary>
public record ResolveIncidentCommandWithReturn(Guid IncidentId): ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}