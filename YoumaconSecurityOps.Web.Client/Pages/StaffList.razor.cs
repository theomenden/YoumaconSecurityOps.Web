namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class StaffList : ComponentBase
{
    #region Injected Services
    [Inject] public IStaffService StaffService { get; init; }

    [Inject] public INotificationService NotificationService { get; init; }

    [Inject] public IndexedDBManager DbManager { get; init; }
    #endregion

    #region Private Fields
    private IEnumerable<StaffReader> _staffMembers = new List<StaffReader>(50);

    private IEnumerable<StaffReader> _gridDisplay = new List<StaffReader>(50);

    private IEnumerable<StaffRole> _staffRoles = new List<StaffRole>(5);

    private IEnumerable<StaffType> _staffTypes = new List<StaffType>(5);

    private DataGrid<StaffReader> _dataGrid = new();

    private Int32 _totalStaffMembers;

    private StaffReader _selectedStaffMember;

    private Blazorise.Modal _modalRef = new();

    private Boolean _isLoading = false;

    private Int32 _selectedStaffRole;

    private Int32 _selectedStaffType;
    #endregion

    private async Task LoadStaffModels()
    {
        _staffMembers = await StaffService.GetStaffMembersAsync(new GetStaffQuery());

        _staffRoles = await StaffService.GetStaffRolesAsync(new GetStaffRolesQuery());

        _staffTypes = await StaffService.GetStaffTypesAsync(new GetStaffTypesQuery());

        _totalStaffMembers = _staffMembers.Count();

        StateHasChanged();
    }

    private static String DetermineDisplayIcon(Boolean statusCheck)
    {
        return statusCheck ? " fa-check-circle text-success" : " fa-times-circle text-danger";
    }

    private async Task OnReadData(DataGridReadDataEventArgs<StaffReader> e)
    {
        await LoadStaffModels();

        if (!e.CancellationToken.IsCancellationRequested)
        {

            _totalStaffMembers = _staffMembers.Count();

            _gridDisplay = _staffMembers.ToList();
        }

        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        foreach (var staffType in  _staffTypes)
        {
            var staffTypeIndexRecord = new StoreRecord<StaffType>
            {
                Storename= "YSecStaffTypes",
                Data = staffType
            };

            await DbManager.AddRecord(staffTypeIndexRecord);
        }
        
        foreach (var staffRole in _staffRoles)
        {
            var staffTypeIndexRecord = new StoreRecord<StaffRole>
            {
                Storename = "YSecRoles",
                Data = staffRole
            };

            await DbManager.AddRecord(staffTypeIndexRecord);
        }
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

    private Task OnFilteredDataChanged(DataGridFilteredDataEventArgs<StaffReader> eventArgs)
    {
        Console.WriteLine($"Filter changed > Items: {eventArgs.FilteredItems}; Total: {eventArgs.TotalItems};");

        return Task.CompletedTask;
    }

    private Task OnSortChanged(DataGridSortChangedEventArgs eventArgs)
    {
        Console.WriteLine($"Sort changed > Field: {eventArgs.FieldName}; Direction: {eventArgs.SortDirection};");

        return Task.CompletedTask;
    }

    private static void OnRowStyling(StaffReader member, DataGridRowStyling styling)
    {
        if (member.IsOnBreak)
        {
            styling.Background = Background.Danger;
        }
    }
    
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

        await NotificationService.Success(response, $"{memberToReturn.Contact.PreferredName} Sent on break");
    }

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

