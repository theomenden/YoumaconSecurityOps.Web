namespace YoumaconSecurityOps.Core.Mediatr.Queries;

public record GetFilteredStaffCount : QueryBase<Int32>
{
    public GetFilteredStaffCount(Boolean isOnBreak, Boolean needsCrashSpace, Boolean isBlackShirt, Boolean isRaveApproved)
    {
        IsOnBreak = isOnBreak;

        NeedsCrashSpace = needsCrashSpace;

        IsBlackShirt = isBlackShirt;

        IsRaveApproved = isRaveApproved;
    }

    public Boolean IsOnBreak { get; }

    public Boolean NeedsCrashSpace { get; } = true;

    public Boolean IsBlackShirt { get; }

    public Boolean IsRaveApproved { get; }
}
