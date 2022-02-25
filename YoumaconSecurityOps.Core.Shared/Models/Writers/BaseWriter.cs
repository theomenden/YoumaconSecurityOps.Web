namespace YoumaconSecurityOps.Core.Shared.Models.Writers;

/// <summary>
/// Basis for all Writing entities in the application -- allows for easy conversion to JSON
/// </summary>
public abstract record BaseWriter: IEntity
{
    protected BaseWriter()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id {get;}
}