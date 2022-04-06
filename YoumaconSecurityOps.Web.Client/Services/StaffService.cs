
namespace YoumaconSecurityOps.Web.Client.Services;

public class StaffService : IStaffService
{
    private readonly YSecServiceOptions _configuration;

    private readonly IMediator _mediator;

    private readonly ILogger<StaffService> _logger;

    private readonly PronounsIndexedDbRepository _pronounsIndexedDbRepository;

    private readonly StaffTypesIndexedDbRepository _staffTypesIndexedDbRepository;

    private readonly StaffRolesIndexedDbRepository _rolesIndexedDbRepository;

    public StaffService(YSecServiceOptions configuration, IMediator mediator, ILogger<StaffService> logger, PronounsIndexedDbRepository pronounsIndexedDbRepository, StaffTypesIndexedDbRepository staffTypesIndexedDbRepository, StaffRolesIndexedDbRepository rolesIndexedDbRepository)
    {
        _configuration = configuration;
        _mediator = mediator;
        _logger = logger;
        _pronounsIndexedDbRepository = pronounsIndexedDbRepository;
        _staffTypesIndexedDbRepository = staffTypesIndexedDbRepository;
        _rolesIndexedDbRepository = rolesIndexedDbRepository;
    }


    #region Query Methods
    public async Task<List<StaffReader>> GetStaffMembersAsync(GetStaffQuery staffQuery, CancellationToken cancellationToken = default)
    {
        return await _mediator.CreateStream(staffQuery, cancellationToken).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<List<StaffReader>>> GetStaffMembersWithResponseAsync(GetStaffQuery staffQuery,
        CancellationToken cancellationToken = default)
    {
        var responseObject = new ApiResponse<List<StaffReader>>();

        try
        {
            var members = await _mediator.CreateStream(staffQuery, cancellationToken)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            responseObject.Outcome = OperationOutcome.SuccessfulOutcome;
            responseObject.Data = members;
            responseObject.ResponseCode = ResponseCodes.ApiSuccess;
        }
        catch (Exception e)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = e.Message;
            responseObject.Outcome = outcome;
            responseObject.ResponseCode = ResponseCodes.ApiError;
            responseObject.ResponseMessage = e.Message;
            responseObject.Data = null;
        }

        return responseObject;
    }
    public async Task<List<StaffRole>> GetStaffRolesAsync(GetStaffRolesQuery rolesQuery, CancellationToken cancellationToken = default)
    {
        if (await IsRolesStoreInvalidatedAsync(cancellationToken))
        {
            await PurgeInvalidRolesStoreAsync(cancellationToken);

            var staffRoles =
                await _mediator.CreateStream(rolesQuery, cancellationToken).ToArrayAsync(cancellationToken);

            await _rolesIndexedDbRepository.CreateOrUpdateMultipleAsync(staffRoles, cancellationToken);
        }

        return await _rolesIndexedDbRepository.GetAllAsync(cancellationToken);
    }

    public async Task<List<StaffType>> GetStaffTypesAsync(GetStaffTypesQuery typesQuery, CancellationToken cancellationToken = default)
    {
        if (await IsStaffTypesStoreInvalidatedAsync(cancellationToken))
        {
            await PurgeInvalidStaffTypesStoreAsync(cancellationToken);

            var staffTypes =
                await _mediator.CreateStream(typesQuery, cancellationToken).ToArrayAsync(cancellationToken);

            await _staffTypesIndexedDbRepository.CreateOrUpdateMultipleAsync(staffTypes, cancellationToken);
        }

        return await _staffTypesIndexedDbRepository.GetAllAsync(cancellationToken);
    }

    public async Task<List<StaffReader>> GetStaffMembersAsync(GetStaffWithParametersQuery staffQuery, CancellationToken cancellationToken = default)
    {
        return await _mediator.CreateStream(staffQuery, cancellationToken).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task<StaffReader> GetStaffMemberAsync(Guid staffId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Adding Methods
    public async Task<ApiResponse<Guid>> AddNewStaffMemberAsync(AddFullStaffEntryCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default)
    {
        var response = new ApiResponse<Guid>();

        try
        {
            response.Data = await _mediator.Send(commandWithReturn, cancellationToken);

            response.Outcome = OperationOutcome.SuccessfulOutcome;

            response.ResponseCode = ResponseCodes.ApiSuccess;
        }
        catch (InvalidOperationException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;

            _logger.LogError("Exception Thrown: {@ex}", ex);
            response.ResponseCode = ResponseCodes.UnrecognizedError;
            response.ResponseMessage = ex.Message;
        }
        catch (AggregateException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;

            _logger.LogError("Exception Thrown: {@ex}", ex);
            response.ResponseCode = ResponseCodes.UnintelligibleResponse;
            response.ResponseMessage = ex.InnerException?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;

            _logger.LogError("Exception Thrown: {@ex}", ex);
            response.ResponseCode = ResponseCodes.HttpError;
            response.ResponseMessage = ex.Message;
        }

        return response;
    }

    public async Task<ApiResponse<Guid>> AddNewStaffMemberAsync(AddStaffCommand commandWithReturn, CancellationToken cancellationToken = default)
    {
        var response = new ApiResponse<Guid>();

        try
        {
            response.Data = await _mediator.Send(commandWithReturn, cancellationToken);
            response.Outcome = OperationOutcome.SuccessfulOutcome;
            response.ResponseCode = ResponseCodes.ApiSuccess;
        }
        catch (InvalidOperationException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }
        catch (AggregateException ex)
        {

            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }
        catch (Exception ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }

        return response;
    }

    public async Task<ApiResponse<Guid>> AddNewStaffTypeRoleMapAsync(AddStaffTypeRoleMapCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = new ApiResponse<Guid>();

        try
        {
            response.Data = await _mediator.Send(command, cancellationToken);
            response.Outcome = OperationOutcome.SuccessfulOutcome;
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
    #endregion

    #region Mutation Methods
    public async Task<ApiResponse<Guid>> UpdateStaffInformationAsync(UpdateStaffInfoCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default)
    {
        var response = new ApiResponse<Guid>();

        try
        {
            response.Data = await _mediator.Send(commandWithReturn, cancellationToken);

            response.ResponseCode = ResponseCodes.ApiSuccess;
        }
        catch (InvalidOperationException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }
        catch (AggregateException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }
        catch (Exception ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }

        return response;
    }

    public async Task<ApiResponse<Guid>> SendStaffMemberOnBreakAsync(SendOnBreakCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default)
    {
        var response = new ApiResponse<Guid>();

        try
        {
            response.Data = await _mediator.Send(commandWithReturn, cancellationToken);

            response.ResponseCode = ResponseCodes.ApiSuccess;
        }
        catch (InvalidOperationException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }
        catch (AggregateException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }
        catch (Exception ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }

        return response;
    }

    public async Task<ApiResponse<Guid>> ReturnStaffMemberFromBreakAsync(ReturnFromBreakCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default)
    {
        var response = new ApiResponse<Guid>();

        try
        {
            response.Data = await _mediator.Send(commandWithReturn, cancellationToken);

            response.ResponseCode = ResponseCodes.ApiSuccess;
        }
        catch (InvalidOperationException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }
        catch (AggregateException ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }
        catch (Exception ex)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;
            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = ex.Message;
            response.Outcome = outcome;
            response.ResponseCode = ResponseCodes.ApiError;
            response.ResponseMessage = ex.Message;
            response.Data = Guid.Empty;
        }

        return response;
    }

    public async Task<IEnumerable<Pronoun>> GetPronounsAsync(GetPronounsQuery pronounsQuery, CancellationToken cancellationToken = default)
    {
        if (await IsPronounsStoreInvalidatedAsync(cancellationToken))
        {
            await PurgeInvalidPronounsStoreAsync(cancellationToken);

            var pronouns = (await _mediator.Send(pronounsQuery, cancellationToken)).ToArray();

            await _pronounsIndexedDbRepository.CreateOrUpdateMultipleAsync(pronouns, cancellationToken);
        }

        return await _pronounsIndexedDbRepository.GetAllAsync(cancellationToken);
    }

    #endregion

    #region Invalidation Methods
    private async Task<Boolean> IsPronounsStoreInvalidatedAsync(CancellationToken cancellationToken = default)
    {
        var isStoreEmpty = await _pronounsIndexedDbRepository.IsEmptyAsync(cancellationToken);

        var isSlidingWindowExpired = DateTime.Now > _configuration.GetSlidingWindowRelativeToNow() ||
            DateTime.Now >= _configuration.GetAbsoluteWindowRelativeToNow();

        return isStoreEmpty || isSlidingWindowExpired;
    }

    private async Task PurgeInvalidPronounsStoreAsync(CancellationToken cancellationToken = default)
    {
        await _pronounsIndexedDbRepository.ClearAsync(cancellationToken);
    }
    private async Task<Boolean> IsStaffTypesStoreInvalidatedAsync(CancellationToken cancellationToken = default)
    {
        var isStoreEmpty = await _staffTypesIndexedDbRepository.IsEmptyAsync(cancellationToken);

        var isSlidingWindowExpired = DateTime.Now > _configuration.GetSlidingWindowRelativeToNow() ||
                                     DateTime.Now >= _configuration.GetAbsoluteWindowRelativeToNow();

        return isStoreEmpty || isSlidingWindowExpired;
    }

    private async Task PurgeInvalidStaffTypesStoreAsync(CancellationToken cancellationToken = default)
    {
        await _staffTypesIndexedDbRepository.ClearAsync(cancellationToken);
    }
    private async Task<Boolean> IsRolesStoreInvalidatedAsync(CancellationToken cancellationToken = default)
    {
        var isStoreEmpty = await _rolesIndexedDbRepository.IsEmptyAsync(cancellationToken);

        var isSlidingWindowExpired = DateTime.Now > _configuration.GetSlidingWindowRelativeToNow() ||
                                     DateTime.Now >= _configuration.GetAbsoluteWindowRelativeToNow();

        return isStoreEmpty || isSlidingWindowExpired;
    }

    private async Task PurgeInvalidRolesStoreAsync(CancellationToken cancellationToken = default)
    {
        await _rolesIndexedDbRepository.ClearAsync(cancellationToken);
    }

    #endregion
}