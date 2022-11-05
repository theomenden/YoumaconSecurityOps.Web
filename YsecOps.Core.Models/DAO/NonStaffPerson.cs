using System;
using System.Collections.Generic;

namespace YsecOps.Core.Models.DAO;

public partial class NonStaffPerson
{
    public Guid Id { get; set; }
    public Guid IncidentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Alias { get; set; }
    public long PhoneNumber { get; set; }
    public int PronounId { get; set; }
    public string Email { get; set; }

    public virtual Incident Incident { get; set; }
    public virtual Pronoun Pronoun { get; set; }
}
