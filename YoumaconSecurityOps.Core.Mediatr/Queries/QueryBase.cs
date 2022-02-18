namespace YoumaconSecurityOps.Core.Mediatr.Queries;

/// <summary>
/// Base class for Queries ;) --therealmkb
/// </summary>
/// <typeparam name="T"></typeparam>
/// <returns>Typically a collection of entities of type <typeparamref name="T"/></returns>
/// <remarks><see cref="IQuery{T}"/></remarks>
public abstract record QueryBase<T>(String Issuer) : IQuery<T>
{
    protected QueryBase()
    : this(typeof(T).FullName)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; }

    public DateTime CreatedAt { get; }

}