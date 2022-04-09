namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddFullStaffEntryCommandWithReturn : ICommandWithReturn<Guid>
{
    public AddFullStaffEntryCommandWithReturn(StaffWriter staffWriter, ContactWriter contactWriter, StaffTypeRoleMapWriter staffTypeRoleMapWriter)
    {
        Id = Guid.NewGuid();

        FullStaffWriter = new (staffWriter, contactWriter, staffTypeRoleMapWriter);
    }

    public FullStaffWriter FullStaffWriter { get; }

    public Guid Id { get; }
}