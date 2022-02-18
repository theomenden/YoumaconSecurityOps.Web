namespace YoumaconSecurityOps.Core.Mediatr.Queries;

/// <summary>
/// <inheritdoc cref="IStreamingQuery{T}"/>
/// </summary>
/// <typeparam name="TEntity">The type the query expects to return as a <see cref="IAsyncEnumerable{T}"/></typeparam>
/// <param name="Issuer">The calling method for the query defaults to the type</param>
public abstract record StreamQueryBase<TEntity>(String Issuer) : IStreamingQuery<TEntity>
{
    protected StreamQueryBase()
        :this(typeof(TEntity).FullName)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; }

    public DateTime CreatedAt { get; }
};

