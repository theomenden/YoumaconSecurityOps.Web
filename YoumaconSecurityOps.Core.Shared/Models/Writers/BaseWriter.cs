namespace YoumaconSecurityOps.Core.Shared.Models.Writers;

/// <summary>
/// Basis for all Writing entities in the application -- allows for easy conversion to JSON
/// </summary>
public record BaseWriter: IEntity
{
    public Guid Id => Guid.NewGuid();
}