using System.Threading;
using Microsoft.Extensions.Logging;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public class RadioScheduleService: IRadioScheduleService
    {
        private readonly IMediator _mediator;

        private readonly ILogger<RadioScheduleService> _logger;

        public RadioScheduleService(IMediator mediator, ILogger<RadioScheduleService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<List<RadioScheduleReader>> GetRadiosAsync(GetRadioSchedule radioScheduleQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(radioScheduleQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public async Task<List<RadioScheduleReader>> GetRadiosAsync(GetRadioScheduleWithParameters radioScheduleParameterQuery,
            CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(radioScheduleParameterQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public Task<RadioScheduleReader> GetRadioAsync(Guid radioId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #region Add Methods
        public async Task<ApiResponse<Guid>> AddRadioAsync(AddRadioCommandWithReturn addRadioCommandWithReturn, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<Guid>();

            try
            {
                response.Data = await _mediator.Send(addRadioCommandWithReturn, cancellationToken);

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
        public async Task<ApiResponse<Guid>> CheckInRadioAsync(CheckInRadioCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default)
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

        public async Task<ApiResponse<Guid>> CheckOutRadioAsync(CheckOutRadioCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default)
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
