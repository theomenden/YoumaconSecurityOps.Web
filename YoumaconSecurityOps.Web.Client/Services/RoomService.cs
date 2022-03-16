namespace YoumaconSecurityOps.Web.Client.Services;

public class RoomService: IRoomService
{
    private readonly IMediator _mediator;

    private readonly ILogger<RoomService> _logger;

    public RoomService(IMediator mediator, ILogger<RoomService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<List<RoomScheduleReader>> GetRoomScheduleAsync(GetRoomScheduleQuery roomScheduleQuery, CancellationToken cancellationToken = default)
    {
        return await _mediator.CreateStream(roomScheduleQuery, cancellationToken).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<Guid>> AddRoomToScheduleAsync(AddRoomCommandWithReturn command, CancellationToken cancellationToken = default)
    {
        var response = new ApiResponse<Guid>();

        try
        {
            response.Data = await _mediator.Send(command, cancellationToken);

            response.ResponseCode = ResponseCodes.ApiSuccess;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Exception Thrown: {@ex}", ex);
            response.ResponseCode = ResponseCodes.UnrecognizedError;
            response.ResponseMessage = ex.Message;
        }
        catch (AggregateException ex)
        {
            _logger.LogError("Exception Thrown: {@ex}", ex);
            response.ResponseCode = ResponseCodes.UnintelligibleResponse;
            response.ResponseMessage = ex.InnerException?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception Thrown: {@ex}", ex);
            response.ResponseCode = ResponseCodes.HttpError;
            response.ResponseMessage = ex.Message;
        }

        return response;
    }
}