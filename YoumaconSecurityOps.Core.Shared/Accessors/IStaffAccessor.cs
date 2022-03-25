namespace YoumaconSecurityOps.Core.Shared.Accessors;

public interface IStaffAccessor: IAccessor<StaffReader>
{
    Task<IEnumerable<Pronouns>> GetAllPronounsAsync(YoumaconSecurityDbContext context, CancellationToken cancellationToken = default);
}