using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;
using YoumaconSecurityOps.Data.EntityFramework.Context;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    internal sealed class ContactRepository: IContactAccessor, IContactRepository
    {
        private readonly ILogger<ContactRepository> _logger;

        private readonly YoumaconSecurityDbContext _dbContext;
        
        public ContactRepository(YoumaconSecurityDbContext dbContext, ILogger<ContactRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IAsyncEnumerable<ContactReader> GetAll(CancellationToken cancellationToken = new ())
        {
            var contacts = _dbContext.Contacts
                .AsAsyncEnumerable()
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.PreferredName)
                .ThenBy(c => c.CreatedOn);

            return contacts;
        }

        public async Task<ContactReader> WithId(Guid entityId, CancellationToken cancellationToken = new ())
        {
            var contact = await GetAll(cancellationToken)
                .SingleOrDefaultAsync(c => c.Id == entityId, cancellationToken);

            return contact;
        }

        public IAsyncEnumerator<ContactReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
        {
            var contactAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

            return contactAsyncEnumerator;
        }

        public async Task<bool> AddAsync(ContactReader entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Contacts.AddAsync(entity, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to store entity {@entity}. Exception : {@ex}", entity, ex);
                return false;
            }
        }
    }
}
