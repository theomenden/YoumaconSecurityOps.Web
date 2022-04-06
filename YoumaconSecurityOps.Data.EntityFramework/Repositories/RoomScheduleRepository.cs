using System.Linq.Expressions;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class RoomScheduleRepository : IRoomScheduleAccessor, IRoomScheduleRepository
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly ILogger<RoomScheduleRepository> _logger;

    public RoomScheduleRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, ILogger<RoomScheduleRepository> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public IAsyncEnumerable<RoomScheduleReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var rooms = dbContext.RoomSchedules
            .AsAsyncEnumerable();

        return rooms;
    }


    public IAsyncEnumerable<RoomScheduleReader> GetAllThatMatchAsync(YoumaconSecurityDbContext dbContext, Expression<Func<RoomScheduleReader, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var rooms = dbContext.RoomSchedules
            .Where(predicate)
            .AsAsyncEnumerable();

        return rooms;
    }

    public IAsyncEnumerable<RoomScheduleReader> GetAvailableRoomsAsync(YoumaconSecurityDbContext context, CancellationToken cancellationToken = default)
    {

        var rooms = context.RoomSchedules 
            .Where(rm => rm.IsCurrentlyOccupied == false)
            .AsAsyncEnumerable();

        return rooms;
    }

    public IAsyncEnumerable<RoomScheduleReader> GetRoomsByStaffIdAsync(YoumaconSecurityDbContext context, Guid staffId, CancellationToken cancellationToken = default)
    {
        var rooms = context.RoomSchedules
            .AsAsyncEnumerable();

        return rooms;
    }

    public async Task<RoomScheduleReader> WithIdAsync(YoumaconSecurityDbContext dbContext, Guid entityId, CancellationToken cancellationToken = default)
    {
        var room = await dbContext.RoomSchedules
            .AsQueryable()
            .FirstOrDefaultAsync(rm => rm.Id == entityId, cancellationToken);

        return room;
    }

    public IAsyncEnumerator<RoomScheduleReader> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        using var context = _dbContextFactory.CreateDbContext();

        var roomScheduleAsyncEnumerator = GetAllAsync(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return roomScheduleAsyncEnumerator;
    }

    public async Task<bool> AddAsync(YoumaconSecurityDbContext dbContext, RoomScheduleReader entity,
        CancellationToken cancellationToken = default)
    {
        bool addResult;

        try
        {
            dbContext.RoomSchedules.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            addResult = true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occured while trying to add Room {roomNumber}: {@ex}", entity.RoomNumber, ex);
            addResult = false;
        }

        return addResult;
    }
}

