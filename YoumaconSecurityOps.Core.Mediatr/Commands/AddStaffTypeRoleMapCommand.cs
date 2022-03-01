namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddStaffTypeRoleMapCommand : ICommandWithReturn<Guid>
{
    public AddStaffTypeRoleMapCommand(StaffTypeRoleMapWriter staffTypeRoleMapWriter)
    {
        Id = Guid.NewGuid();

        StaffTypeRoleMapWriter = staffTypeRoleMapWriter;
    }

    public Guid Id { get; }

    public StaffTypeRoleMapWriter StaffTypeRoleMapWriter { get; }

}

