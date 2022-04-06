namespace YoumaconSecurityOps.Web.Client.Services;
internal class ContactService: IContactService
{
    private readonly ILogger<ContactService> _logger;

    private readonly IMediator _mediator;

    public ContactService(ILogger<ContactService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;   
    }

    public async Task<List<ContactReader>> GetContactsAsync(GetContactsQuery query, CancellationToken cancellationToken = default)
    {
        return await _mediator.CreateStream(query, cancellationToken).ToListAsync(cancellationToken);
    }

    public async Task<ApiResponse<Guid>> AddContactInformationAsync(AddContactCommand command, CancellationToken cancellationToken = default)
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

