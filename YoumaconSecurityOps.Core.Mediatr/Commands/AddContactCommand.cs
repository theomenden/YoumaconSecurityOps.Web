namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddContactCommand : ICommandWithReturn<Guid>
{
    public AddContactCommand(ContactWriter contactInformation)
    {
        Id = Guid.NewGuid();
        ContactInformation = contactInformation;
    }

    public Guid Id { get; }

    public ContactWriter ContactInformation { get; }
}