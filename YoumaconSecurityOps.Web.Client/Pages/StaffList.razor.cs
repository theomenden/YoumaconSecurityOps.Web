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
    private IList<StaffReader> _staffMembers = new List<StaffReader>(50);

    private IEnumerable<StaffReader> _gridDisplay = new List<StaffReader>(50);

    private IEnumerable<StaffRole> _staffRoles = new List<StaffRole>(5);

    private IEnumerable<StaffType> _staffTypes = new List<StaffType>(5);

    private IEnumerable<Pronoun> _pronouns = new List<Pronoun>(14);

    private DataGrid<StaffReader> _dataGrid = new();

    private Int32 _totalStaffMembers;

    private ErrorBoundary? _errorBoundary;

    private StaffReader _selectedStaffMember;
    
    private Blazorise.Modal _modalRef = new();

    private SpinKit _spinKitRef = new();

    private Boolean _isLoading = false;

    private Boolean? _isRaveApproved;

    private Boolean _isBlackShirt;

    private Int32 _selectedPronoun;

    private Int32 _selectedStaffRole;

    private Int32 _selectedStaffType;

    private Int32 _selectedTypeFilter;

    private Int32 _selectedRoleFilter;
    #endregion

    protected override void OnParametersSet()
    {
        _errorBoundary?.Recover();
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadStaffModels();
    }

    #region DataGrid Configuration Methods
    private async Task LoadStaffModels(CancellationToken cancellationToken = default)
    {
        _staffMembers = await StaffService.GetStaffMembersAsync(new GetStaffQuery(), cancellationToken);
        
        _staffRoles = await StaffService.GetStaffRolesAsync(new GetStaffRolesQuery(), cancellationToken);

        _staffTypes = await StaffService.GetStaffTypesAsync(new GetStaffTypesQuery(), cancellationToken);

        _pronouns = await StaffService.GetPronounsAsync(new GetPronounsQuery(), cancellationToken);

        _totalStaffMembers = await GetStaffMemberCount();
    }

    private async Task OnReadData(DataGridReadDataEventArgs<StaffReader> e)
    {
        await LoadStaffModels(e.CancellationToken);

        if (!e.CancellationToken.IsCancellationRequested)
        {
            var sortDeterminant = new DataGridHelpers<StaffReader>(e);

            var columnInformation =
                sortDeterminant.ColumnStates.Where(cs =>
                    cs.SortDirection is not SortDirection.Default).ToList();

            _totalStaffMembers = _staffMembers.Count;

            _gridDisplay = _staffMembers
                .ToList();
        }
    }

    private Task<Int32> GetStaffMemberCount() => StaffService.GetStaffCountAsync(new GetStaffMemberCount());

    private static void OnStaffNewItemDefaultSetter(StaffReader member)
    {
        member.ContactInformation = new ContactReader();
        member.StaffTypesRoles = new List<StaffTypesRole>(1);
    }

    private static string SetPopupTitle(PopupTitleContext<StaffReader> context)
    {
        var popupTitle = context.LocalizationString + " Staff ";

        if (context.EditState is DataGridEditState.Edit)
        {
            popupTitle += $" {context.Item.ContactInformation.PreferredName ?? context.Item.ContactInformation.FirstName}";
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

    private void OnPronounsChanged(Int32 pronounId)
    {
        _selectedPronoun = pronounId;

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
            await NotificationService.Error(response, $"Error with sending {memberToSendOnBreak.ContactInformation.PreferredName} on break!");
            return;
        }

        await NotificationService.Success(response, $"{memberToSendOnBreak.ContactInformation.PreferredName} Sent on break");

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
            await NotificationService.Error(response, $"Error with returning {memberToReturn.ContactInformation.PreferredName} from break!");
            return;
        }

        await NotificationService.Success(response, $"{memberToReturn.ContactInformation.PreferredName} returned from their break");

        StateHasChanged();
    }
    #endregion
    #region Filtering Methods

    private bool OnStaffTypeFilter(object itemValue, object searchValue)
    {
        var itemId = _staffTypes
            .FirstOrDefault(s => !String.IsNullOrWhiteSpace(s?.Title) && s.Title.Equals(itemValue.ToString()))
            ?.Id ?? 0;

        if (searchValue is int typeFilter)
        {
            return typeFilter == 0 || typeFilter == itemId;
        }

        return true;
    }

    private bool OnStaffRoleFilter(object itemValue, object searchValue)
    {
        var itemId = _staffRoles
            .FirstOrDefault(s => !String.IsNullOrWhiteSpace(s?.Name) && s.Name.Equals(itemValue.ToString()))
            ?.Id ?? 0;

        if (searchValue is int roleFilter)
        {
            return roleFilter == 0 || roleFilter == Convert.ToInt32(itemId);
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

    private bool IsRaveApprovedFilter(object itemValue, object searchValue)
    {
        if (searchValue is bool isRaveApprovedFilter)
        {
            return isRaveApprovedFilter || false == Convert.ToBoolean(_isRaveApproved);
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

    private void OnClearFilterClicked()
    {
        _selectedRoleFilter = 0;
        _selectedStaffType = 0;
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

