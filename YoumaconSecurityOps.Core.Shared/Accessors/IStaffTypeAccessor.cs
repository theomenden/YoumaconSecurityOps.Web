using System.Collections.Generic;

namespace YoumaconSecurityOps.Core.Shared.Accessors;

public interface IStaffTypeAccessor: IEnumerable<StaffType>
{
    /// <summary>
    /// Retrieves the <see cref="StaffType"/>s in the database as a single operation, asynchronously
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="DbContext"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/> where <c>T</c> is <seealso cref="IEnumerable{T}"/> : <seealso cref="StaffType"/></returns>
    Task<IEnumerable<StaffType>> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a single <see cref="StaffType"/> specified by the <param name="staffTypeId"></param> from the database
    /// </summary>
    /// <param name="dbContext">The caller supplied dbContext</param>
    /// <param name="staffTypeId">The staffTypeId to look up</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult}"/> where <c>TResult</c> is <seealso cref="StaffType"/></returns>
    Task<StaffType> WithId(YoumaconSecurityDbContext dbContext, Int32 staffTypeId, CancellationToken cancellationToken = default);
}