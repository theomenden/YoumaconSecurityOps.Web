namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

/// <summary>
/// Base class for "Readers" 
/// </summary>
public abstract class BaseReader: IEntity
{
    /// <value>
    /// Primary Key value for most tables in the database
    /// </value>
    public Guid Id { get; set; }
}