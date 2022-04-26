namespace YoumaconSecurityOps.Web.Client.Models;

/// <summary>
/// Container for relevant session information
/// </summary>
public class SessionModel
{
    public SessionModel()
    {
        Id = Guid.NewGuid();
    }

    /// <value>
    /// The unique Id for this session
    /// </value>
    public Guid Id { get; }

    /// <value>
    /// The name associated with the session
    /// </value>
    public string SessionName { get; set; }

    /// <value>
    /// The id of the circuit associated with the session
    /// </value>
    public string CircuitId { get; set; }

    /// <value>
    /// The time the session was created
    /// </value>
    /// <remarks>Could be used to cause a timeout</remarks>
    public DateTime CreatedAt { get; set; }
}
