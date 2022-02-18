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
    public class StaffService : IStaffService
    {
        private readonly IMediator _mediator;

        private readonly ILogger<StaffService> _logger;

        public StaffService(ILogger<StaffService> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentException("Could not be injected", nameof(logger));

            _mediator = mediator ?? throw new ArgumentException("Could not be injected", nameof(mediator));
        }

        #region Query Methods
        public async Task<List<StaffReader>> GetStaffMembersAsync(GetStaffQuery staffQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(staffQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public async Task<List<StaffRole>> GetStaffRolesAsync(GetStaffRolesQuery rolesQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(rolesQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public async Task<List<StaffType>> GetStaffTypesAsync(GetStaffTypesQuery typesQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(typesQuery, cancellationToken).ToListAsync(cancellationToken);
        }

        public async Task<List<StaffReader>> GetStaffMembersAsync(GetStaffWithParametersQuery staffQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(staffQuery, cancellationToken).ToListAsync(cancellationToken);
        }
        #endregion


        public Task<StaffReader> GetStaffMemberAsync(Guid staffId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #region Adding Methods
        public async Task<ApiResponse<Guid>> AddNewStaffMemberAsync(AddFullStaffEntryCommand command, CancellationToken cancellationToken = default)
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
        public async Task<ApiResponse<Guid>> UpdateStaffInformationAsync(UpdateStaffInfoCommand command, CancellationToken cancellationToken = default)
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

        public async Task<ApiResponse<Guid>> SendStaffMemberOnBreakAsync(SendOnBreakCommand command, CancellationToken cancellationToken = default)
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

        public async Task<ApiResponse<Guid>> ReturnStaffMemberFromBreakAsync(ReturnFromBreakCommand command, CancellationToken cancellationToken = default)
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
