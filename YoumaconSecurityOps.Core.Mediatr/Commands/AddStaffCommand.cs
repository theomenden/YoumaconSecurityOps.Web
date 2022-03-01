namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddStaffCommand: ICommandWithReturn<Guid>
{
    public AddStaffCommand(StaffWriter staffWriter)
    {
        StaffWriter = staffWriter;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; }

    public StaffWriter StaffWriter { get; }
}