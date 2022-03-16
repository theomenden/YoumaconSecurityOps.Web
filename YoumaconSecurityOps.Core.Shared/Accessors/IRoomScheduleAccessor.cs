
namespace YoumaconSecurityOps.Core.Shared.Accessors;
public interface IRoomScheduleAccessor: IAccessor<RoomScheduleReader>
{
    IAsyncEnumerable<RoomScheduleReader> GetRoomsByStaffIdAsync(YoumaconSecurityDbContext context, Guid staffId,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<RoomScheduleReader> GetAvailableRoomsAsync(YoumaconSecurityDbContext context,
        CancellationToken cancellationToken = default);
}