namespace YoumaconSecurityOps.Core.Shared.Accessors;

public interface IStaffRoleAccessor : IEnumerable<StaffRole>
{
    /// <summary>
    /// Retrieves the <see cref="StaffRole"/>s from the database as an asynchronous stream
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="DbContext"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/> where <c>T</c> is <seealso cref="IEnumerable{T}" /> <seealso cref="StaffRole"/></returns>
    public Task<IEnumerable<StaffRole>> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a single <see cref="StaffRole"/>
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="DbContext"/></param>
    /// <param name="staffRoleId">The StaffRole id to search</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult}"/> where <c>TResult</c> is <seealso cref="StaffRole"/></returns>
    Task<StaffRole> WithId(YoumaconSecurityDbContext dbContext, int staffRoleId, CancellationToken cancellationToken = default);
}