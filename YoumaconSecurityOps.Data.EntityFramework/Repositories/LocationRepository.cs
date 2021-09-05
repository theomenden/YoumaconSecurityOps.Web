using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;
using YoumaconSecurityOps.Data.EntityFramework.Context;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    internal sealed class LocationRepository: ILocationAccessor, ILocationRepository
    {
        private readonly YoumaconSecurityDbContext _dbContext;

        public LocationRepository(YoumaconSecurityDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException("Could not register DbContext: ", nameof(dbContext));
        }

        public IAsyncEnumerable<LocationReader> GetAll(CancellationToken cancellationToken = new())
        {
            var locations = _dbContext.Locations
                .AsAsyncEnumerable()
                .OrderBy(l => l.Name);

            return locations;
        }
        
        public async Task<LocationReader> WithId(Guid entityId, CancellationToken cancellationToken = new())
        {
            var location = await _dbContext.Locations.AsQueryable().SingleOrDefaultAsync(l => l.Id == entityId, cancellationToken);

            return location;
        }

        public IAsyncEnumerable<LocationReader> GetHotels(CancellationToken cancellationToken = new())
        {
            var hotels = GetAll(cancellationToken)
                .Where(l => l.IsHotel)
                .OrderBy(l => l.Name);

            
            return hotels;
        }

        public IAsyncEnumerator<LocationReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
        {
            var locationAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

            return locationAsyncEnumerator;
        }

        public async Task<bool> AddAsync(LocationReader entity, CancellationToken cancellationToken = new())
        {
            try
            {
                await _dbContext.Locations.AddAsync(entity, cancellationToken);

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
