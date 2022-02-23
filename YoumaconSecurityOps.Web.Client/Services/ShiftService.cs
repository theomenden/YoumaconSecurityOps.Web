using System.Threading;
using Microsoft.Extensions.Logging;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public class ShiftService: IShiftService
    {
        private readonly IMediator _mediator;

        private readonly ILogger<ShiftService> _logger;

        public ShiftService(IMediator mediator, ILogger<ShiftService> logger)
        {
            _mediator = mediator ?? throw new ArgumentException("Could not be injected", nameof(mediator));
            _logger = logger;
        }

        #region Query Methods
        public async Task<List<ShiftReader>> GetShiftsAsync(GetShiftListQuery shiftQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(shiftQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public async Task<List<ShiftReader>> GetShiftsAsync(GetShiftListWithParametersQuery shiftQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(shiftQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public Task<ShiftReader> GetShiftAsync(Guid shiftId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Add Methods
        public async Task<ApiResponse<Guid>> AddShiftAsync(AddShiftCommand addShiftCommand, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<Guid>();

            try
            {
                response.Data = await _mediator.Send(addShiftCommand, cancellationToken);

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
        public async Task<ApiResponse<Guid>> CheckIn(ShiftCheckInCommand command, CancellationToken cancellationToken = default)
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

        public async Task<ApiResponse<Guid>> CheckOut(ShiftCheckoutCommand command, CancellationToken cancellationToken = default)
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

        public async Task<ApiResponse<Guid>> ReportIn(ShiftReportInCommand command, CancellationToken cancellationToken = default)
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
    }
}
