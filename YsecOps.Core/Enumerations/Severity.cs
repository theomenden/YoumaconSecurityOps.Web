using TheOmenDen.Shared.Enumerations;

namespace YoumaconSecurityOps.Core.Shared.Enumerations;

/// <summary>
/// The Severity of a given incident
/// </summary>
public sealed record Severity(String Name, Int32 Id) : EnumerationBase<Severity>(Name, Id)
{
    ///<value>
    /// Simple incident to resolve
    /// </value>
    public static readonly Severity Minor = new(nameof(Minor),1);
    ///<value>
    /// Required a minor level of elevation
    /// </value>
    public static readonly Severity BlackShirt = new(nameof(BlackShirt), 2);
    ///<value>
    /// Required elevation to the team's captain
    /// </value>
    public static readonly Severity Captain = new(nameof(Captain), 3);
    ///<value>
    /// Required elevation to the administrators
    /// </value>
    public static readonly Severity Admin = new(nameof(Admin), 4);
    ///<value>
    /// Required elevation to the Assistant Department Head
    /// </value>
    public static readonly Severity Adh = new(nameof(Adh), 5);
    ///<value>
    /// Required elevation to the department head
    /// </value>
    public static readonly Severity Dh = new(nameof(Dh), 6);
    ///<value>
    /// Required Police Interaction or a Police report
    /// </value>
    public static readonly Severity Police = new(nameof(Police), 7);
    ///<value>
    /// Incident was Resolved
    /// </value>
    public static readonly Severity Resolved = new(nameof(Resolved), 8);
}