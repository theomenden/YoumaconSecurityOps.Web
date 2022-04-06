using Blazorise.DataGrid.Configuration;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class ShiftLog : ComponentBase
{
    #region Injected Services
    [Inject] public IShiftService ShiftService { get; init; }

    [Inject] public ILocationService LocationService { get; init; }

    [Inject] public IStaffService StaffService { get; init; }

    [Inject] public IMessageService MessageService { get; init; }

    [Inject] public INotificationService Notifications { get; init; }
    #endregion
    #region Fields
    private IEnumerable<ShiftReader> _shifts = new List<ShiftReader>(200);

    private List<ShiftReader> _gridDisplay = new(200);

    private IEnumerable<StaffReader> _staffMembers = new List<StaffReader>(200);

    private IEnumerable<LocationReader> _locations = new List<LocationReader>(15);

    private ShiftReader _selectedShift;

    private Int32 _totalShifts;

    private DataGrid<ShiftReader> _dataGrid = new();

    private Blazorise.Bootstrap5.Modal _modalRef;

    private Guid _selectedStaffMember = Guid.Empty;

    private Guid _selectedStartingLocation = Guid.Empty;

    private DateTime? _selectedStartDate;
    private DateTime? _selectedEndDate;

    private Boolean _isLoading;

    private readonly VirtualizeOptions _virtualizeOptions = new() { OverscanCount = 5 };
    #endregion
    #region DataGrid Configuration Methods
    private async Task LoadShiftData()
    {
        _locations = await LocationService.GetLocationsAsync(new GetLocationsQuery());

        _staffMembers = await StaffService.GetStaffMembersAsync(new GetStaffQuery());

        _shifts = await ShiftService.GetShiftsAsync(new GetShiftListQuery());
    }

    private async Task OnReadData(DataGridReadDataEventArgs<ShiftReader> e)
    {
        await LoadShiftData();
        
        if (!e.CancellationToken.IsCancellationRequested)
        {
            var sortDeterminant = new DataGridHelpers<ShiftReader>(e);

            var columnToSort =
                sortDeterminant.ColumnStates.FirstOrDefault(cs =>
                    cs.SortDirection is not SortDirection.Default) ?? new ColumnState { Field = nameof(ShiftReader.StartAt), SortDirection = SortDirection.Ascending };

            _totalShifts = _shifts.Count();

            _gridDisplay = _shifts
                .AsQueryable()
                .DynamicSort(columnToSort)
                .ToList();
        }

        StateHasChanged();
    }

    private static void OnRowStyling(ShiftReader shift, DataGridRowStyling styling)
    {
        var staffOnShift = shift.StaffMember;

        if (shift.IsLate)
        {
            styling.Background = Background.Warning;
        }

        if (staffOnShift?.IsOnBreak ?? false)
        {
            styling.Background = Background.Danger;
        }
    }

    private static String DetermineDisplayIcon(Boolean statusCheck)
    {
        return statusCheck ? " fa-check-circle text-success" : " fa-times-circle text-danger";
    }

    private static string SetPopupTitle(PopupTitleContext<ShiftReader> context)
    {
        var popupTitle = context.LocalizationString + " Shift ";

        if (context.EditState is DataGridEditState.Edit)
        {
            popupTitle += $"for {context.Item.StaffMember.ContactInformation.PreferredName} {context.Item.StaffMember.ContactInformation.LastName}";
        }

        return popupTitle;
    }

    private Task Reset()
    {
        return _dataGrid.Reload();
    }
    #endregion
    #region SelectList Methods
    private void OnStartDateChanged(DateTime? date)
    {
        _selectedStartDate = date;
    }

    private void OnEndDateChanged(DateTime? date)
    {
        _selectedEndDate = date;
    }

    private void OnSelectedStaffMemberChanged(Guid value)
    {
        _selectedStaffMember = value;

        StateHasChanged();
    }

    private void OnSelectedStartingLocationChanged(Guid value)
    {
        _selectedStartingLocation = value;

        StateHasChanged();
    }
    #endregion
    #region DataGrid Mutation Methods
    private async Task OnRowInserting(CancellableRowChange<ShiftReader, Dictionary<string, object>> newShift)
    {
        var staffMemberAssigned = _staffMembers.First(st => st.Id == _selectedStaffMember);

        var startingLocation = _locations.First(l => l.Id == (_selectedStartingLocation));

        var addShiftCommand = new AddShiftCommandWithReturn(_selectedStartDate.GetValueOrDefault(DateTime.Now), _selectedEndDate.GetValueOrDefault(DateTime.Now),
            staffMemberAssigned.Id, staffMemberAssigned.ContactInformation.PreferredName, startingLocation.Id);

        var addedEntityResponse = await ShiftService.AddShiftAsync(addShiftCommand);

        if (addedEntityResponse.ResponseCode is not ResponseCodes.ApiSuccess)
        {
            await Notifications.Error(new MarkupString($"<em>{addedEntityResponse.ResponseMessage}</em>"), "Failed to add shift");

            return;
        }

        newShift.Item.Id = addedEntityResponse.Data;
        newShift.Item.StaffMember = staffMemberAssigned;
        newShift.Item.StaffMember.ContactInformation = staffMemberAssigned.ContactInformation;
        newShift.Item.StartingLocation = startingLocation;
        newShift.Item.CurrentLocation = startingLocation;
        newShift.Item.StartAt = _selectedStartDate.GetValueOrDefault();
        newShift.Item.EndAt = _selectedEndDate.GetValueOrDefault();

        await Notifications.Success(new MarkupString($"<em>{addedEntityResponse.ResponseMessage}</em>"),
            "Successfully Added Shift");

        StateHasChanged();
    }

    private async Task OnCheckedIn(Guid shiftId)
    {
        _isLoading = true;

        var checkInCommand = new ShiftCheckInCommandWithReturn(shiftId);

        var checkedInResponse = await ShiftService.CheckIn(checkInCommand);

        var markUpResponse = new MarkupString($"<em>{checkedInResponse.ResponseMessage}</em>");

        if (checkedInResponse.ResponseCode is not ResponseCodes.ApiSuccess)
        {
            await Notifications.Error(markUpResponse, "Failed to add shift");

            _isLoading = false;

            return;
        }

        await Notifications.Success(markUpResponse, "Checked In Successfully");

        _isLoading = false;

        StateHasChanged();
    }

    private async Task OnCheckedOut(ShiftReader shiftToCheckOut)
    {
        _isLoading = true;

        if (await MessageService.Confirm($"This will checkout {shiftToCheckOut.StaffMember.ContactInformation.PreferredName} from their shift", "Are you sure?"))
        {
            var checkedOutCommand = new ShiftCheckoutCommandWithReturn(shiftToCheckOut.Id);

            var checkedOutResponse = await ShiftService.CheckOut(checkedOutCommand);

            var markUpResponse = new MarkupString($"<em>{checkedOutResponse.ResponseMessage}</em>");

            if (checkedOutResponse.ResponseCode is not ResponseCodes.ApiSuccess)
            {
                await Notifications.Error(markUpResponse, "Failed to add shift");
                _isLoading = false;
                return;
            }

            await Notifications.Success(markUpResponse, "Checked Out Successfully");
        }

        _isLoading = false;

        StateHasChanged();
    }

    private async Task OnReportingIn(Guid shiftId)
    {
        _isLoading = true;

        var reportInCommand = new ShiftReportInCommandWithReturn(shiftId, _selectedStartingLocation);

        var reportedInResponse = await ShiftService.ReportIn(reportInCommand);

        var markUpResponse = new MarkupString($"<em>{reportedInResponse.ResponseMessage}</em>");

        if (reportedInResponse.ResponseCode is not ResponseCodes.ApiSuccess)
        {
            await Notifications.Error(markUpResponse, "Failed to add shift");

            _isLoading = false;

            return;
        }

        await Notifications.Success(markUpResponse, "Reported In Successfully");
        _isLoading = false;

        StateHasChanged();
    }
    #endregion
    #region Edit Form Methods
    private void ShowModal(Guid shiftId)
    {
        _selectedShift = _shifts.Single(sh => sh.Id == shiftId);

        _modalRef.Show();
    }

    private void HideModal()
    {
        _modalRef.Hide();
    }
    #endregion
}

