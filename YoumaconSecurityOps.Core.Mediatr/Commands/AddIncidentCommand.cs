namespace YoumaconSecurityOps.Core.Mediatr.Commands;

/// <summary>
/// Contains parameters that will allow for the creation of an <see cref="IncidentWriter"/>
/// </summary>
public record AddIncidentCommand : ICommand<Guid>
{
    /// <value>
    /// Staff Member's ID who recorded the incident
    /// </value>
    public Guid RecordedById { get; init; }
        
    /// <value>
    /// Staff Member's ID who reported the incident
    /// </value>
    public Guid ReportedById { get; init; }

    /// <value>
    /// Shift that the incident occurred under
    /// </value>
    public Guid ShiftId { get; init; }

    /// <value>
    /// Location where the incident occurred
    /// </value>
    public Guid LocationId { get; init; }

    /// <value>
    /// Initial Severity level for the incident
    /// </value>
    public Severity Severity { get; init; }

    /// <value>
    /// Title of the incident
    /// </value>
    public String Title { get; init; }

    /// <value>
    /// Detailed Description of the incident
    /// </value>
    public String Description { get; init; }

    /// <value>
    /// Tracking AggregateId for the command
    /// </value>
    public Guid Id => Guid.NewGuid();
}