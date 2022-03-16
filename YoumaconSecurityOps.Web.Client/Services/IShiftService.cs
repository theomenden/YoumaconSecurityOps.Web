﻿using System.Threading;
using YoumaconSecurityOps.Core.Mediatr.Commands;

namespace YoumaconSecurityOps.Web.Client.Services;

public interface IShiftService
{
    #region Query Methods
    /// <summary>
    /// <para>Retrieves all of the shifts in the database as an asynchronous stream</para>
    /// </summary>
    /// <param name="shiftQuery"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult}"/>: <seealso cref="List{T}"/> : <seealso cref="ShiftReader"/></returns>
    /// <remarks><paramref name="shiftQuery"/> is an empty query that has no parameters</remarks>
    Task<List<ShiftReader>> GetShiftsAsync(GetShiftListQuery shiftQuery, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para>Retrieves all of the shifts in the database as an asynchronous stream</para>
    /// </summary>
    /// <param name="shiftQuery"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult"/>: <seealso cref="List{T}"/> : <seealso cref="ShiftReader"/></returns>
    /// <remarks>Review: <see cref="GetShiftListWithParametersQuery"/></remarks>
    Task<List<ShiftReader>> GetShiftsAsync(GetShiftListWithParametersQuery shiftQuery, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a particular shift from the database with an id that matches <paramref name="shiftId"/>
    /// </summary>
    /// <param name="shiftId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult}"/>: <seealso cref="ShiftReader"/></returns>
    Task<ShiftReader> GetShiftAsync(Guid shiftId, CancellationToken cancellationToken = default);
    #endregion

    #region Create Methods
    /// <summary>
    /// Adds a new shift record to the database with the information provided from the <see cref="AddShiftCommandWithReturn"/>
    /// </summary>
    /// <param name="addShiftCommandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult}"/>: <seealso cref="ResponseCodes"/></returns>
    /// <remarks>This method should catch all exceptions and return them as an appropriate <see cref="ResponseCodes"/></remarks>
    Task<ApiResponse<Guid>> AddShiftAsync(AddShiftCommandWithReturn addShiftCommandWithReturn, CancellationToken cancellationToken = default);
    #endregion

    #region Mutation Methods
    /// <summary>
    /// Takes the supplied <paramref name="commandWithReturn"/> and uses the supplied <see cref="ShiftReader"/>-Id to check in the shift.
    /// </summary>
    /// <param name="commandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult}"/>: <seealso cref="ApiResponse{T}"/>: <seealso cref="Guid"/> - the shift that was checked in</returns>
    Task<ApiResponse<Guid>> CheckIn(ShiftCheckInCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Takes the supplied <paramref name="commandWithReturn"/> and uses the supplied <see cref="ShiftReader"/>-Id to check out the shift.
    /// </summary>
    /// <param name="commandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult}"/>: <seealso cref="ApiResponse{T}"/>: <seealso cref="Guid"/> - the shift that was checked out</returns>
    Task<ApiResponse<Guid>> CheckOut(ShiftCheckoutCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Takes the supplied <paramref name="commandWithReturn"/> and uses the supplied <see cref="ShiftReader"/>-Id to report current status for the shift.
    /// </summary>
    /// <param name="commandWithReturn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{TResult}"/>: <seealso cref="ApiResponse{T}"/>: <seealso cref="Guid"/> - the shift that was had it's status recently reported</returns>
    Task<ApiResponse<Guid>> ReportIn(ShiftReportInCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);
    #endregion
}