namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ReturnFromBreakCommandWithReturn : ICommandWithReturn<Guid>
{
    public ReturnFromBreakCommandWithReturn(Guid staffId)
    {
        Id = Guid.NewGuid();
        StaffId = staffId;
    }

    public Guid Id {get; }

    public Guid StaffId { get; }

}