namespace YoumaconSecurityOps.Core.Shared.Accessors;

public interface IStaffAccessor: IAccessor<StaffReader>
{
    Task<IEnumerable<Pronoun>> GetAllPronounsAsync(YoumaconSecurityDbContext context, CancellationToken cancellationToken = default);
}