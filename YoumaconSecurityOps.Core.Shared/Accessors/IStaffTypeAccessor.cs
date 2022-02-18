namespace YoumaconSecurityOps.Core.Shared.Accessors;

public interface IStaffTypeAccessor: IAsyncEnumerable<StaffType>
{
    IAsyncEnumerable<StaffType> GetAll(CancellationToken cancellationToken = default);

    Task<StaffType> WithId(Int32 staffTypeId, CancellationToken cancellationToken = default);
}