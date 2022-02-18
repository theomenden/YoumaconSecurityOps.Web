namespace YoumaconSecurityOps.Core.Shared.Parameters;

/// <summary>
/// <para>It provides a base entity for queries from the api</para>
/// <para>IsHistoricalQuery tells us if we are dealing with system versioned data</para>
/// <inheritdoc cref="IEntity"/>
/// </summary>
public abstract record QueryStringParameters(bool IsHistoricalQuery = false) : IEntity
{
    public Guid Id => Guid.NewGuid();
}