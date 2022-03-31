using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YoumaconSecurityOps.Web.Client.Services;

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
    /// Uses a <paramref name="staffQuery"/> with no parameters to retrieve an asynchronous stream of <see cref="StaffReader"/>
    /// </summary>
    /// <param name="staffQuery">The Provided parameterless query</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ApiResponse{T}"/></returns>
    Task<ApiResponse<List<StaffReader>>> GetStaffMembersWithResponseAsync(GetStaffQuery staffQuery,
        CancellationToken cancellationToken = default);

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

    /// <summary>
    /// Retrieves pronouns stored in the database
    /// </summary>
    /// <param name="pronounsQuery">Provided parameterless query</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <see cref="List{T}"/>: <see cref="Pronouns"/></returns>
    Task<IEnumerable<Pronouns>> GetPronounsAsync(GetPronounsQuery pronounsQuery, CancellationToken cancellationToken = default);
    #endregion
    #region Add Methods
    /// <summary>
    /// Adds a <see cref="StaffReader"/> to the underlying database
    /// </summary>
    /// <param name="commandWithReturn"><see cref="AddStaffCommand"/> supplied by the caller</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Guid"/> of the created <see cref="StaffReader"/></returns>
    Task<ApiResponse<Guid>> AddNewStaffMemberAsync(AddStaffCommand commandWithReturn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a <see cref="StaffReader"/> to the underlying api - with the Full Properties for the staff member
    /// </summary>
    /// <param name="commandWithReturn"><see cref="AddFullStaffEntryCommandWithReturn"/> supplied by the caller</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Guid"/> of the created <see cref="StaffReader"/></returns>
    Task<ApiResponse<Guid>> AddNewStaffMemberAsync(AddFullStaffEntryCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a <see cref="StaffTypesRoles"/> mapping to the api
    /// </summary>
    /// <param name="command"><<see cref="AddStaffTypeRoleMapCommand"/> supplied by the caller</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Guid"/> of the created <see cref="StaffTypesRoles"/></returns>
    Task<ApiResponse<Guid>> AddNewStaffTypeRoleMapAsync(AddStaffTypeRoleMapCommand command, CancellationToken cancellationToken = default);
    #endregion
    #region Mutation Methods
    /// <summary>
    /// <paramref name="commandWithReturn" /> contains information to update for the staff member - including contact information.
    /// </summary>
    /// <param name="commandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/></returns>
    Task<ApiResponse<Guid>> UpdateStaffInformationAsync(UpdateStaffInfoCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends the provided staff member on break.
    /// </summary>
    /// <param name="commandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/></returns>
    Task<ApiResponse<Guid>> SendStaffMemberOnBreakAsync(SendOnBreakCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the provided staff member from their break.
    /// </summary>
    /// <param name="commandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/></returns>
    Task<ApiResponse<Guid>> ReturnStaffMemberFromBreakAsync(ReturnFromBreakCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);
    #endregion
}