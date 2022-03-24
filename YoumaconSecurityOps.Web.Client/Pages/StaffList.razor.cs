using Blazorise.DataGrid.Configuration;
using Blazorise.SpinKit;
using Microsoft.AspNetCore.Components.Web;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class StaffList : ComponentBase
{
    #region Injected Services
    [Inject] public IStaffService StaffService { get; init; }

    [Inject] public INotificationService NotificationService { get; init; }
    
    #endregion
    #region Private Fields
    private IEnumerable<StaffReader> _staffMembers = new List<StaffReader>(50);

    private IEnumerable<StaffReader> _gridDisplay = new List<StaffReader>(50);

    private IEnumerable<StaffRole> _staffRoles = new List<StaffRole>(5);

    private IEnumerable<StaffType> _staffTypes = new List<StaffType>(5);

    private DataGrid<StaffReader> _dataGrid = new();

    private Int32 _totalStaffMembers;

    private ErrorBoundary? _errorBoundary;

    private ApiResponse<List<StaffReader>> _apiResponse;

    private StaffReader _selectedStaffMember;

    private Blazorise.Modal _modalRef = new();

    private SpinKit _spinKitRef = new();

    private Boolean _isLoading = false;

    private Boolean _isBlackShirt;

    private Int32 _selectedStaffRole;

    private Int32 _selectedStaffType;

    private Int32 _selectedTypeFilter = 0;

    private Int32 _selectedRoleFilter = 0;


    private readonly VirtualizeOptions _virtualizeOptions = new() { OverscanCount = 5 };
    #endregion

    protected override void OnParametersSet()
    {
        _errorBoundary?.Recover();
    }

    #region DataGrid Configuration Methods
    private async Task LoadStaffModels(CancellationToken cancellationToken = default)
    {
        _apiResponse = await StaffService.GetStaffMembersWithResponseAsync(new GetStaffQuery(), cancellationToken);

        _staffMembers = _apiResponse.Data;

        _staffRoles = await StaffService.GetStaffRolesAsync(new GetStaffRolesQuery(), cancellationToken);

        _staffTypes = await StaffService.GetStaffTypesAsync(new GetStaffTypesQuery(), cancellationToken);
        
        StateHasChanged();
    }

    private static String DetermineDisplayIcon(Boolean statusCheck)
    {
        return statusCheck ? " fa-check-circle text-success" : " fa-times-circle text-danger";
    }

    private async Task OnReadData(DataGridReadDataEventArgs<StaffReader> e)
    {
        await LoadStaffModels(e.CancellationToken);

        if (!e.CancellationToken.IsCancellationRequested)
        {
            var sortDeterminant = new DataGridHelpers<StaffReader>(e);

            var columnToSort =
                sortDeterminant.ColumnStates.Where(cs =>
                    cs.SortDirection is not SortDirection.Default).ToList();

            _totalStaffMembers = _staffMembers.Count();

            _gridDisplay = _staffMembers
                .DynamicFilter(columnToSort)
                .DynamicSort(columnToSort)
                .ToList();
        }

        StateHasChanged();
    }

    private static void OnStaffNewItemDefaultSetter(StaffReader member)
    {
        member.Contact = new ContactReader();
        member.StaffTypeRoleMaps = new List<StaffTypesRoles>(1);
    }

    private static string SetPopupTitle(PopupTitleContext<StaffReader> context)
    {
        var popupTitle = context.LocalizationString + " Staff ";

        if (context.EditState is DataGridEditState.Edit)
        {
            popupTitle += $" {context.Item.Contact.PreferredName ?? context.Item.Contact.FirstName}";
        }

        return popupTitle;
    }

    private Task ResetGrid()
    {
        return _dataGrid.Reload();
    }

    private static void OnRowStyling(StaffReader member, DataGridRowStyling styling)
    {
        if (member.IsOnBreak)
        {
            styling.Background = Background.Danger;
        }
    }
    #endregion
    #region Staff Mutation Methods
    private void OnSelectedStaffRoleChanged(Int32 staffRole)
    {
        _selectedStaffRole = staffRole;

        StateHasChanged();
    }

    private void OnSelectedStaffTypeChanged(Int32 staffType)
    {
        _selectedStaffType = staffType;

        StateHasChanged();
    }

    private async Task SendMemberOnBreak(Guid staffId)
    {
        var memberToSendOnBreak = _staffMembers.Single(s => s.Id == staffId);


        var command = new SendOnBreakCommandWithReturn(memberToSendOnBreak.Id);

        var status = await StaffService.SendStaffMemberOnBreakAsync(command);

        var response = new MarkupString(status.ResponseMessage);

        if (status.Data == Guid.Empty)
        {
            await NotificationService.Error(response, $"Error with sending {memberToSendOnBreak.Contact.PreferredName} on break!");
            return;
        }

        await NotificationService.Success(response, $"{memberToSendOnBreak.Contact.PreferredName} Sent on break");

        StateHasChanged();
    }

    private async Task OnReturnedFromBreak(Guid staffId)
    {
        var memberToReturn = _staffMembers.Single(s => s.Id == staffId);

        var command = new ReturnFromBreakCommandWithReturn(memberToReturn.Id);

        var status = await StaffService.ReturnStaffMemberFromBreakAsync(command);

        var response = new MarkupString(status.ResponseMessage);

        if (status.Data == Guid.Empty)
        {
            await NotificationService.Error(response, $"Error with returning {memberToReturn.Contact.PreferredName} from break!");
            return;
        }

        await NotificationService.Success(response, $"{memberToReturn.Contact.PreferredName} returned from their break");

        StateHasChanged();
    }
    #endregion
    #region Filtering Methods

    private static bool OnStaffTypeFilter(object itemValue, object searchValue)
    {
        if (searchValue is int typeFilter)
        {
            return typeFilter == 0 || typeFilter == Convert.ToInt32(itemValue);
        }

        return true;
    }

    private static bool OnStaffRoleFilter(object itemValue, object searchValue)
    {
        if (searchValue is int roleFilter)
        {
            return roleFilter == 0 || roleFilter == Convert.ToInt32(itemValue);
        }

        return true;
    }

    private static bool OnBlackShirtFilter(object itemValue, object searchValue)
    {
        if (searchValue is bool blackShirtFilter)
        {
            return blackShirtFilter || false == Convert.ToBoolean(itemValue);
        }

        return true;
    }

    private static bool IsRaveApprovedFilter(object itemValue, object searchValue)
    {
        if (searchValue is bool isRaveApprovedFilter)
        {
            return isRaveApprovedFilter || false == Convert.ToBoolean(itemValue);
        }

        return true;
    }

    private static bool NeedsCrashSpaceFilter(object itemValue, object searchValue)
    {
        if (searchValue is bool needsCrashSpaceFilter)
        {
            return needsCrashSpaceFilter || false == Convert.ToBoolean(itemValue);
        }

        return true;
    }

    private static bool NotOnBreakFilter(object itemValue, object searchValue)
    {
        if (searchValue is bool breakFilter)
        {
            return !breakFilter || Convert.ToBoolean(itemValue);
        }

        return false;
    }
    #endregion
    #region Edit Form Methods
    private void ShowModal(Guid incidentId)
    {
        _selectedStaffMember = _staffMembers.Single(i => i.Id == incidentId);

        _modalRef.Show();
    }

    private void HideModal()
    {
        _modalRef.Hide();
    }
    #endregion
}

