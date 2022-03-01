using System.Threading;
using Microsoft.Extensions.Logging;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public class IncidentService: IIncidentService
    {
        private readonly IMediator _mediator;

        private readonly ILogger<IncidentService> _logger;

        public IncidentService(IMediator mediator, ILogger<IncidentService> logger)
        {
            _mediator = mediator ?? throw new ArgumentException("Could not be injected", nameof(mediator));
            _logger = logger;
        }

        #region Query Methods
        public async Task<List<IncidentReader>> GetIncidentsAsync(GetIncidentsQuery incidentListQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(incidentListQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public async Task<List<IncidentReader>> GetIncidentsAsync(GetIncidentsWithParametersQuery incidentListQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(incidentListQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public Task<IncidentReader> GetIncidentAsync(Guid incidentId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Add Methods
        public async Task<ApiResponse<Guid>> AddIncidentAsync(AddIncidentCommandWithReturn addIncidentCommandWithReturn, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<Guid>();

            try
            {
                response.Data = await _mediator.Send(addIncidentCommandWithReturn, cancellationToken);

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
        public async Task<ApiResponse<Guid>> ResolveIncidentAsync(ResolveIncidentCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default)
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

        public async Task<ApiResponse<Guid>> AdjustIncidentSeverityAsync(AdjustIncidentSeverityCommandWithReturn commandWithReturn,
            CancellationToken cancellationToken = default)
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
