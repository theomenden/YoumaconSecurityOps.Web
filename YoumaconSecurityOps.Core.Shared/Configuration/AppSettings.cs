namespace YoumaconSecurityOps.Core.Shared.Configuration;

/// <summary>
/// Container for Settings stored in appsettings.json to be deserialized into
/// </summary>
public record AppSettings
{
    /// <value>
    /// The Connection string for the security operations database
    /// </value>
    public string YoumaDbConnectionString { get; init; }

    /// <value>
    /// The connection string for the Event Store/Auditing Database
    /// </value>
    public string EventStoreConnectionString { get; init; }

    ///<value>
    /// The connection string for the authentication/authorization Database
    /// </value>
    public string YSecItAuthConnectionString { get; init; }

    /// <value>
    /// The expiration interval for an access expiration
    /// </value>
    public int AccessExpiration { get; init; }

    /// <value>
    /// The interval of time for each refresh of an expiration
    /// </value>
    public int RefreshExpiration { get; init; }

    /// <value>
    /// The application's authorized Audience - typically *
    /// </value>
    public string Audience { get; init; }

    /// <value>
    /// The issuer for the tokens
    /// </value>
    public string Issuer { get; init; }

    /// <value>
    /// The secret value for tokens
    /// </value>
    public string Secret { get; init; }
}