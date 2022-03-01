namespace YoumaconSecurityOps.Web.Client.Services;

/// <summary>
/// Contains definitions for <see cref="ContactReader"/> methods in the api
/// </summary>
public interface IContactService
{
    #region Get Methods
    /// <summary>
    /// Retrieves all the stored <see cref="ContactReader"/>s from the api
    /// </summary>
    /// <param name="query">The supplied parameter-less query</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An async stream of <see cref="ContactReader"/>s</returns>
    public Task<List<ContactReader>> GetContactsAsync(GetContactsQuery query, CancellationToken cancellationToken = default);
    #endregion

    #region Add Methods
    /// <summary>
    /// Adds a <see cref="ContactReader"/> to it's underlying table in the api
    /// </summary>
    /// <param name="command">Command supplied by the caller</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The <seealso cref="Guid"/> from the created <seealso cref="ContactReader"/></returns>
    public Task<ApiResponse<Guid>> AddContactInformationAsync(AddContactCommand command, CancellationToken cancellationToken = default);
    #endregion

    #region Mutation Methods

    //public Task<ApiResponse<Guid>> UpdateContactInformationAsync(UpdateContactCommand command,
    //    CancellationToken cancellationToken = default);
    #endregion
}

