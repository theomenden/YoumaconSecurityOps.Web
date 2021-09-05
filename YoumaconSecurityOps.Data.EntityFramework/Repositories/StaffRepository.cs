using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;
using YoumaconSecurityOps.Data.EntityFramework.Context;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    internal sealed class StaffRepository : IStaffAccessor, IStaffRepository
    {
        private readonly YoumaconSecurityDbContext _dbContext;

        private readonly ILogger<StaffRepository> _logger;

        public StaffRepository(YoumaconSecurityDbContext dbContext, ILogger<StaffRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;   
        }

        public IAsyncEnumerable<StaffReader> GetAll(CancellationToken cancellationToken = new())
        {
            var staff =
                from member in _dbContext.StaffMembers.AsAsyncEnumerable()
                join contact in _dbContext.Contacts on member.ContactId equals contact.Id
                join typeRoleMap in _dbContext.StaffTypesRoles on member.StaffTypeRoleId equals typeRoleMap.Id
                join staffType in _dbContext.StaffTypes on typeRoleMap.StaffTypeId equals staffType.Id
                join staffRole in _dbContext.StaffRoles on typeRoleMap.RoleId equals staffRole.Id
                orderby new { staffType, staffRole, contact.LastName }
                select new StaffReader
                {
                    Id = member.Id,
                    Contact = contact,
                    BreakEndAt = member.BreakEndAt,
                    BreakStartAt = member.BreakStartAt,
                    IsBlackShirt = member.IsBlackShirt,
                    IsOnBreak = member.IsOnBreak,
                    IsRaveApproved = member.IsRaveApproved,
                    NeedsCrashSpace = member.NeedsCrashSpace,
                    ShirtSize = member.ShirtSize,
                    StaffTypeRoleMaps = new List<StaffTypesRoles>(3){typeRoleMap}
                    
                };

            return staff;
        }

        public async Task<StaffReader> WithId(Guid entityId, CancellationToken cancellationToken = new())
        {
            var staffMember = await GetAll(cancellationToken)
                .FirstOrDefaultAsync(s => s.Id == entityId, cancellationToken);

            return staffMember;
        }

        public IAsyncEnumerator<StaffReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
        {
            var staffAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

            return staffAsyncEnumerator;
        }

        public async Task<bool> AddAsync(StaffReader entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.StaffMembers.AddAsync(entity, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occured while attempting to create a staff record for: {@staffMember}, {ex}", entity, ex.InnerException?.Message ?? ex.Message);

                return false;
            }
        }

        public async Task<StaffReader> SendOnBreak(Guid staffId, CancellationToken cancellationToken = default)
        {
            var staffMemberToSendOnBreak = await
                _dbContext.StaffMembers.AsQueryable().SingleOrDefaultAsync(st => st.Id == staffId, cancellationToken);

            try
            {

                if (staffMemberToSendOnBreak is not null)
                {
                    staffMemberToSendOnBreak.BreakStartAt = DateTime.Now;
                    staffMemberToSendOnBreak.IsOnBreak = true;

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occured trying to send staff member {staffId} on break: {ex}",
                    ex.InnerException?.Message ?? ex.Message);
            }

            return staffMemberToSendOnBreak;
        }

        public async Task<StaffReader> ReturnFromBreak(Guid staffId, CancellationToken cancellationToken = default)
        {
            var staffMemberToReturnFromBreak = await
                _dbContext.StaffMembers.AsQueryable().SingleOrDefaultAsync(st => st.Id == staffId, cancellationToken);

            try
            {

                if (staffMemberToReturnFromBreak is not null)
                {
                    staffMemberToReturnFromBreak.BreakStartAt = DateTime.Now;
                    staffMemberToReturnFromBreak.IsOnBreak = true;

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occured trying to return staff member {staffId} from their break: {ex}",
                    ex.InnerException?.Message ?? ex.Message);
            }

            return staffMemberToReturnFromBreak;
        }
    }
}
