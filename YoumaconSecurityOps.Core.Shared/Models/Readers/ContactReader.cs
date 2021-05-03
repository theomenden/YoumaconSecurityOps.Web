using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public sealed class ContactReader: BaseReader, IEquatable<ContactReader>
    {
    public string Email { get; set; }

    public string FirstName { get; set; }
        
    public string LastName { get; set; }

    public string FacebookName { get; set; }

    public string PreferredName { get; set; }

    public DateTime CreatedOn { get; set; }

    public long PhoneNumber { get; set; }

    public Guid StaffId { get; set; }


    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        return obj is ContactReader other && Equals(other);
    }

    public override int GetHashCode()
    {
        return 37 * Id.GetHashCode();
    }

    public bool Equals(ContactReader other)
    {
        if (other is null)
        {
            return false;
        }

        if (PhoneNumber == other.PhoneNumber)
        {
            return true;
        }

        if (Email.Equals(other.Email))
        {
            return true;
        }

        return Id == other.Id;
    }
    }
}
