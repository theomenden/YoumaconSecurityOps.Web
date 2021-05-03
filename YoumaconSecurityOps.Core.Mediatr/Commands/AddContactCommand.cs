using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record AddContactCommand(DateTime CreatedOn, string Email, string FirstName, string LastName,
        string FacebookName, string PreferredName, long PhoneNumber) : ICommand
    {
        public Guid Id => Guid.NewGuid();
    }
}
