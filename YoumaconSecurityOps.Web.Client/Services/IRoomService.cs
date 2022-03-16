namespace YoumaconSecurityOps.Web.Client.Services;

/// <summary>
/// Contains CRUD methods for <see cref="RoomScheduleReader"/>s
/// </summary>
public interface IRoomService
{
    /// <summary>
    /// Issues a <see cref="GetRoomScheduleQuery"/> to retrieve an asynchronous stream of Rooms
    /// </summary>
    /// <param name="roomScheduleQuery">Parameterless Query that was passed in</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="List{T}"/>: <seealso cref="RoomScheduleReader"/></returns>
    Task<List<RoomScheduleReader>> GetRoomScheduleAsync(GetRoomScheduleQuery roomScheduleQuery, CancellationToken cancellationToken = default);

    /// <summary>
    /// Issues an <see cref="AddRoomCommandWithReturn"/> to eventually add a new entry to the room schedule
    /// </summary>
    /// <param name="command">Contains the parameters to build a <see cref="RoomScheduleWriter"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ApiResponse"/>: <see cref="Guid"/> to evaluate the operation outcome</returns>
    Task<ApiResponse<Guid>> AddRoomToScheduleAsync(AddRoomCommandWithReturn command, CancellationToken cancellationToken = default);
}