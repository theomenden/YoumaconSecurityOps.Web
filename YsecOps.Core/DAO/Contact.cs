using System;
using System.Collections.Generic;

namespace YsecOps.Core.Models.DAO;

public partial class Contact
{
    public Guid Id { get; set; }
    public Guid Staff_Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public int Pronoun_Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long PhoneNumber { get; set; }
    public string Email { get; set; }
    public string FacebookName { get; set; }
    public string PreferredName { get; set; }

    public virtual Pronoun Pronoun { get; set; }
    public virtual Staff Staff { get; set; }
}
