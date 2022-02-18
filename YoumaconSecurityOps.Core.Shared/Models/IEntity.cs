namespace YoumaconSecurityOps.Core.Shared.Models;

/// <summary>
/// Simple interface for defining entities in the YoumaconSecurityOps Application
/// </summary>
public interface IEntity
{
    /// <value>
    /// Primary Key definition for most entities throughout the application
    /// </value>
    Guid Id { get; }
}