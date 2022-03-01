namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record CheckOutRadioCommandWithReturn(Guid RadioId, Guid StaffCheckedOutToId, Guid LocationCheckedOutFromId) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}