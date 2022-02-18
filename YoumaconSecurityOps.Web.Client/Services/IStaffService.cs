using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Web.Client.Models;

namespace YoumaconSecurityOps.Web.Client.Services
{
    /// <summary>
    /// Contains methods relating to <see cref="StaffReader"/> Retrieval and storage
    /// </summary>
    public interface IStaffService
    {
        #region Query Methods
        /// <summary>
        /// Uses a <paramref name="staffQuery" /> with no parameters to retrieve an asynchronous stream <see cref="StaffReader"/> 
        /// </summary>
        /// <param name="staffQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="StaffReader"/> </returns>
        /// <remarks><paramref name="staffQuery" /> is a parameterless <see cref="GetStaffQuery" /></remarks>
        Task<List<StaffReader>> GetStaffMembersAsync(GetStaffQuery staffQuery, CancellationToken cancellationToken = default);

        /// <summary>
        /// Uses a <paramref name="staffQuery" /> with no parameters to retrieve an asynchronous stream <see cref="StaffReader"/> 
        /// </summary>
        /// <param name="staffQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="StaffReader"/> </returns>
        Task<List<StaffReader>> GetStaffMembersAsync(GetStaffWithParametersQuery staffQuery,
            CancellationToken cancellationToken = default);

        Task<StaffReader> GetStaffMemberAsync(Guid staffId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an asynchronous stream of the staff roles stored in the database, based off of the provided <paramref name="rolesQuery"/>
        /// </summary>
        /// <param name="rolesQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task{T}"/>: <seealso cref="List{T}"/>: <seealso cref="StaffRole"/></returns>
        Task<List<StaffRole>> GetStaffRolesAsync(GetStaffRolesQuery rolesQuery, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an asynchronous stream of the staff types stored in the database, based off of the provided <paramref name="typesQuery" />
        /// </summary>
        /// <param name="typesQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task{T}"/>: <seealso cref="List{T}"/>: <seealso cref="StaffType"/></returns>
        Task<List<StaffType>> GetStaffTypesAsync(GetStaffTypesQuery typesQuery, CancellationToken cancellationToken = default);
        #endregion
        #region Add Methods
        Task<ApiResponse<Guid>> AddNewStaffMemberAsync(AddFullStaffEntryCommand command, CancellationToken cancellationToken = default);
        #endregion
        #region Mutation Methods
        /// <summary>
        /// <paramref name="command" /> contains information to update for the staff member - including contact information.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/></returns>
        Task<ApiResponse<Guid>> UpdateStaffInformationAsync(UpdateStaffInfoCommand command, CancellationToken cancellationToken =default);

        /// <summary>
        /// Sends the provided staff member on break.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/></returns>
        Task<ApiResponse<Guid>> SendStaffMemberOnBreakAsync(SendOnBreakCommand command, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the provided staff member from their break.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/></returns>
        Task<ApiResponse<Guid>> ReturnStaffMemberFromBreakAsync(ReturnFromBreakCommand command, CancellationToken cancellationToken = default);
        #endregion
    }
}
