using System;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    /// <summary>
    /// Command to resolve a particular incident given by its <paramref name="IncidentId"/>
    /// </summary>
    public record ResolveIncidentCommand(Guid IncidentId): ICommand<Guid>
    {
        public Guid Id => Guid.NewGuid();
    }
}
