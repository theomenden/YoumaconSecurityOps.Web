namespace YoumaconSecurityOps.Web.Client.Services
{
    public class StaffService : IStaffService
    {
        private readonly IMediator _mediator;

        private readonly ILogger<StaffService> _logger;

        public StaffService(ILogger<StaffService> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentException(@"Could not be injected", nameof(logger));

            _mediator = mediator ?? throw new ArgumentException(@"Could not be injected", nameof(mediator));
        }

        #region Query Methods
        public async Task<List<StaffReader>> GetStaffMembersAsync(GetStaffQuery staffQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(staffQuery, cancellationToken).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<StaffRole>> GetStaffRolesAsync(GetStaffRolesQuery rolesQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(rolesQuery, cancellationToken).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<StaffType>> GetStaffTypesAsync(GetStaffTypesQuery typesQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(typesQuery, cancellationToken).ToListAsync(cancellationToken).ConfigureAwait(false);
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

        public async Task<ApiResponse<Guid>> AddNewStaffMemberAsync(AddStaffCommand commandWithReturn, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<Guid>();

            try
            {
                response.Data = await _mediator.Send(commandWithReturn, cancellationToken);

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

        public async Task<ApiResponse<Guid>> AddNewStaffTypeRoleMapAsync(AddStaffTypeRoleMapCommand command,
            CancellationToken cancellationToken = default)
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
    }
}
