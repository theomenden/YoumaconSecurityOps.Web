namespace YoumaconSecurityOps.Core.Shared.Accessors;

public interface IStaffRoleAccessor: IAsyncEnumerable<StaffRole>
{
    public IAsyncEnumerable<StaffRole> GetAll(CancellationToken cancellationToken = default);

    Task<StaffRole> WithId(int staffRoleId, CancellationToken cancellationToken = default);
}