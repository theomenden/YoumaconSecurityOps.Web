using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Enumerations;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Web.Client.Models;

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
            var radios = await _mediator.Send(radioScheduleQuery, cancellationToken);

            return await radios.ToListAsync(cancellationToken);
        }

        public async Task<List<RadioScheduleReader>> GetRadiosAsync(GetRadioScheduleWithParameters radioScheduleParameterQuery,
            CancellationToken cancellationToken = default)
        {
            var radios = await _mediator.Send(radioScheduleParameterQuery, cancellationToken);

            return await radios.ToListAsync(cancellationToken);
        }

        public Task<RadioScheduleReader> GetRadioAsync(Guid radioId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #region Add Methods
        public async Task<ApiResponse<Guid>> AddRadioAsync(AddRadioCommand addRadioCommand, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<Guid>();

            try
            {
                response.Data = await _mediator.Send(addRadioCommand, cancellationToken);

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
        public async Task<ApiResponse<Guid>> CheckInRadioAsync(CheckInRadioCommand command, CancellationToken cancellationToken = default)
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

        public async Task<ApiResponse<Guid>> CheckOutRadioAsync(CheckOutRadioCommand command, CancellationToken cancellationToken = default)
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
