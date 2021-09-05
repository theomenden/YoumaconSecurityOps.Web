using System;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record AddContactCommand(DateTime CreatedOn, string Email, string FirstName, string LastName,
        string FacebookName, string PreferredName, long PhoneNumber) : ICommand
    {
        public Guid Id => Guid.NewGuid();
    }
}
