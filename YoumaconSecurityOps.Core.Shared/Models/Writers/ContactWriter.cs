using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Writers
{
    public record ContactWriter(DateTime CreatedOn, string Email, string FirstName, string LastName,
        string FacebookName, string PreferredName, long PhoneNumber) : BaseWriter;
}
