using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    internal sealed class StaffRepository : IStaffAccessor, IStaffRepository
    {
        private readonly YoumaconSecurityDbContext _dbContext;

        //private readonly ILogger<StaffRepository> _logger;

        public StaffRepository(YoumaconSecurityDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IAsyncEnumerable<StaffReader> GetAll(CancellationToken cancellationToken = new())
        {
            var staff =
                from member in _dbContext.StaffMembers.AsAsyncEnumerable()
                join contact in _dbContext.Contacts.AsAsyncEnumerable() on member.ContactId equals contact.Id
                join role in _dbContext.StaffRoles.AsAsyncEnumerable() on member.RoleId equals role.Id
                join type in _dbContext.StaffTypes.AsAsyncEnumerable() on member.StaffTypeId equals type.Id
                select new StaffReader
                {
                    ContactInformation = contact,
                    Role = role,
                    StaffType = type
                };

            return staff;
        }

        public async Task<StaffReader> WithId(Guid entityId, CancellationToken cancellationToken = new())
        {
            var staffMember = await GetAll(cancellationToken).FirstOrDefaultAsync(s => s.Id == entityId, cancellationToken);

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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
