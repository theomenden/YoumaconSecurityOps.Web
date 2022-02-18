namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

/// <summary>
/// <para>The read only representation for a shift</para>
/// <para>This class reflects how a Shift is stored in the Database</para>
/// <para>And also how a shift is read/serialized in JSON</para>
/// <inheritdoc cref="BaseReader"/>
/// </summary>
[Table("Shifts")]
public partial class ShiftReader: BaseReader
{
    public ShiftReader()
        :base()
    {
            
    }

    #region Basic Properties
    /// <value>
    /// Id for the <see cref="StaffReader"/> who owns this shift
    /// </value>
    public Guid StaffId { get; set; }

    /// <value>
    /// Created Date - Made By Database for History Table
    /// </value>
    public DateTime SysStart { get; set; }

    /// <value>
    /// Ending Record Date - Made by Database for history table
    /// </value>
    public DateTime SysEnd { get; set; }

    /// <value>
    /// Date and Time the shift is scheduled to end
    /// </value>
    public DateTime EndAt { get; set; }

    /// <value>
    /// Date and Time the shift is scheduled to begin
    /// </value>
    public DateTime StartAt { get; set; }

    /// <value>
    /// ID of the location where the shift is supposed to start
    /// </value>
    public Guid StartingLocationId { get; set; }

    /// <value>
    /// ID of the location where the shift is currently
    /// </value>
    public Guid CurrentLocationId { get; set; }

    /// <value>
    /// Time the staff member checked in to this shift
    /// </value>
    public DateTime? CheckedInAt { get; set; }

    /// <value>
    /// Time the staff member checked out from this shift
    /// </value>
    public DateTime? CheckedOutAt { get; set; }

    /// <value>
    /// Time the staff member last reported their status to this shift
    /// </value>
    public DateTime? LastReportedAt { get; set; }

    /// <value>
    /// Any remaining notes on this shift
    /// </value>
    public string Notes { get; set; }
    #endregion

    #region NavigationProperties
    /// <value>
    /// Reference to where the shift is currently
    /// </value>
    public virtual LocationReader CurrentLocation { get; set; }

    /// <value>
    /// Reference to the Staff Member associated with this shift
    /// </value>
    public virtual StaffReader StaffMember { get; set; }
        
    /// <value>
    /// Reference to the location where the shift was to start
    /// </value>
    public virtual LocationReader StartingLocationNavigation { get; set; }
        
    /// <value>
    /// Reference to any amount of incidents that have happened during the shift
    /// </value>
    /// <remarks>This is a collection - <see cref="IEnumerable{T}"/>: <seealso cref="IncidentReader"/></remarks>
    public virtual IEnumerable<IncidentReader> Incidents { get; set; }
    #endregion

    #region WorkingShiftInformation
    /// <value>
    /// Late flag for the staff member
    /// </value>
    /// <returns><code>true</code> if the Staff member is more than 5 minutes late <code>false</code> if they were on time or early</returns>
    [NotMapped]
    public Boolean IsLate => CheckIfMemberWasLate();

    /// <value>
    /// The current amount of the shift that has been worked for display purposes
    /// </value>
    /// <returns><see cref="TimeSpan"/></returns>
    [NotMapped]
    public TimeSpan WorkedTimeSoFar => WorkedShiftDuration();
    #endregion

    #region PrivateFunctions

    private Boolean CheckIfMemberWasLate()
    {
        var isLate = false;

        if (CheckedInAt.HasValue)
        {
            isLate = CheckedInAt.Value >= StartAt.AddMinutes(5);
        }

        return isLate;
    }

    private TimeSpan TotalShiftDuration()
    {
        return EndAt - StartAt;
    }

    private TimeSpan WorkedShiftDuration()
    {
        var currentTime = DateTime.Now;

        return currentTime - CheckedInAt.GetValueOrDefault(currentTime);
    }

    #endregion
}