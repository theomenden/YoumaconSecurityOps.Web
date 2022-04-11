namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching.Invalidations;

public class LocationsCacheInvalidator: DistributedCacheInvalidator<AddLocationCommand, GetLocationsQuery, IEnumerable<LocationReader>>
    {
        public LocationsCacheInvalidator(ICache<GetLocationsQuery, IEnumerable<LocationReader>> cache) 
            : base(cache)
        {
        }

        protected override string GetCacheKeyIdentifier(AddLocationCommand request)
        {
            return request.Name;
        }
    }