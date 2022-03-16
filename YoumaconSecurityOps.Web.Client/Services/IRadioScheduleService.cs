using System.Threading;

namespace YoumaconSecurityOps.Web.Client.Services;

public interface IRadioScheduleService
{
    #region Query Methods
    /// <summary>
    ///  Returns an asynchronous stream of the radios in the database
    /// </summary>
    /// <param name="radioScheduleQuery"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="List{T}"/>: <seealso cref="RadioScheduleReader"/></returns>
    /// <remarks><see cref="GetRadioSchedule" /> is an empty class</remarks>
    Task<List<RadioScheduleReader>> GetRadiosAsync(GetRadioSchedule radioScheduleQuery, CancellationToken cancellationToken = default);

    /// <summary>
    ///  Returns an asynchronous stream of the radios in the database
    /// </summary>
    /// <param name="radioScheduleParameterQuery"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="List{T}"/>: <seealso cref="RadioScheduleReader"/></returns>
    /// <remarks><see cref="GetRadioScheduleWithParameters"/> defines the search parameters used to filter the radios down</remarks>
    Task<List<RadioScheduleReader>> GetRadiosAsync(GetRadioScheduleWithParameters radioScheduleParameterQuery, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an individual radio from the database
    /// </summary>
    /// <param name="radioId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="RadioScheduleReader"/></returns>
    Task<RadioScheduleReader> GetRadioAsync(Guid radioId, CancellationToken cancellationToken = default);
    #endregion

    #region Add Methods
    /// <summary>
    /// Adds a new Radio entity to the radio schedule table
    /// </summary>
    /// <param name="addRadioCommandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/>: <seealso cref="Guid"/></returns>
    /// <remarks><see cref="ApiResponse{T}"/> is used to determine if operation was successful, or encountered an error on adding</remarks>
    Task<ApiResponse<Guid>> AddRadioAsync(AddRadioCommandWithReturn addRadioCommandWithReturn, CancellationToken cancellationToken = default);
    #endregion

    #region Mutation Methods
    /// <summary>
    /// Checks in a Radio entity from a <see cref="StaffReader"/>
    /// </summary>
    /// <param name="commandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/>: <seealso cref="Guid"/></returns>
    /// <remarks><see cref="ApiResponse{T}"/> is used to determine if operation was successful, or encountered an error during the check in process</remarks>
    Task<ApiResponse<Guid>> CheckInRadioAsync(CheckInRadioCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks out a Radio entity to a <see cref="StaffReader"/>
    /// </summary>
    /// <param name="commandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="ApiResponse{T}"/>: <seealso cref="Guid"/></returns>
    /// <remarks><see cref="ApiResponse{T}"/> is used to determine if operation was successful, or encountered an error during the check out process</remarks>
    Task<ApiResponse<Guid>> CheckOutRadioAsync(CheckOutRadioCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);
    #endregion
}