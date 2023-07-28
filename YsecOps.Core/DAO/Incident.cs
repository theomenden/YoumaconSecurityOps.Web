using System;
using System.Collections.Generic;

namespace YsecOps.Core.Models.DAO;

public partial class Incident
{
    public Incident()
    {
        NonStaffPeople = new HashSet<NonStaffPerson>();
    }

    public Guid Id { get; set; }
    public Guid RecordedBy_Id { get; set; }
    public Guid OpsManager_Id { get; set; }
    public Guid Shift_Id { get; set; }
    public Guid Location_Id { get; set; }
    public int Severity { get; set; }
    public DateTime RecordedOn { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public bool IsFollowUpRequired { get; set; }
    public string FollowUpResponse { get; set; }
    public string Injuries { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public virtual Location Location { get; set; }
    public virtual Staff OpsManager { get; set; }
    public virtual Staff RecordedBy { get; set; }
    public virtual Shift Shift { get; set; }
    public virtual ICollection<NonStaffPerson> NonStaffPeople { get; set; }
}
