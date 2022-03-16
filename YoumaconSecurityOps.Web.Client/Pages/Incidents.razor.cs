using Microsoft.AspNetCore.Components.Web;
using YoumaconSecurityOps.Core.Shared.Responses;
using YoumaconSecurityOps.Web.Client.Invariants;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Incidents : ComponentBase
{
    #region Injected Services
    [Inject] public IIncidentService IncidentService { get; set; }

    [Inject] public IShiftService ShiftService { get; set; }

    [Inject] public IStaffService StaffService { get; set; }

    [Inject] public ILocationService LocationService { get; set; }

    [Inject] public INotificationService NotificationService { get; set; }
    #endregion

    #region Private Fields
    private IEnumerable<StaffReader> _staffMembers = new List<StaffReader>(50);

    private IEnumerable<LocationReader> _locations = new List<LocationReader>(50);

    private IEnumerable<ShiftReader> _shifts = new List<ShiftReader>(50);

    private IEnumerable<IncidentReader> _incidents = new List<IncidentReader>(50);

    private IEnumerable<IncidentReader> _gridDisplay = new List<IncidentReader>(50);

    private DataGrid<IncidentReader> _dataGrid = new();

    private ErrorBoundary? _errorBoundary;

    private ApiResponse _apiResponse;

    private Blazorise.Modal _modalRef;

    private Int32 _totalIncidents = 0;

    private IncidentReader _selectedIncident;

    private Int32 _selectedSeverity;

    private Guid _selectedReportingStaffMember = Guid.Empty;
    private Guid _selectedRecordingStaffMember = Guid.Empty;

    private Guid _selectedOccurrenceLocation = Guid.Empty;

    private Guid _selectedShift = Guid.Empty;

    private DateTime? _selectedRecordedDate;

    private Boolean _isLoading = false;
    #endregion

    protected override void OnParametersSet()
    {
        _errorBoundary?.Recover();
    }

    #region DataGrid Configuration Methods
    private async Task LoadIncidentData(CancellationToken cancellationToken = default)
    {
        try
        {
            _locations = await LocationService.GetLocationsAsync(new GetLocationsQuery(), cancellationToken);

            _staffMembers = await StaffService.GetStaffMembersAsync(new GetStaffQuery(), cancellationToken);

            _shifts = await ShiftService.GetShiftsAsync(new GetShiftListQuery(), cancellationToken);

            _incidents = await IncidentService.GetIncidentsAsync(new GetIncidentsQuery(), cancellationToken);
        }
        catch (Exception e)
        {
            var outcome = OperationOutcome.UnsuccessfulOutcome;

            outcome.CorrelationId = Guid.NewGuid().ToString();
            outcome.Message = Errors.UnhandledError;

            _apiResponse = new ()
            {
                Outcome = outcome,
                ResponseCode = ResponseCodes.ApiError,
                ResponseMessage = e.Message
            };
            
            throw;
        }
    }

    private async Task OnReadData(DataGridReadDataEventArgs<IncidentReader> e)
    {
        await LoadIncidentData(e.CancellationToken);

        if (!e.CancellationToken.IsCancellationRequested)
        {
            var sortDeterminant = new DataGridHelpers<IncidentReader>(e);

            var columnToSort =
                sortDeterminant.ColumnStates.FirstOrDefault(cs =>
                    cs.SortDirection is not SortDirection.Default) ?? new ColumnState { Field = nameof(IncidentReader.Severity), SortDirection = SortDirection.Ascending };

            _totalIncidents = _incidents.Count();

            _gridDisplay = _incidents
                .AsQueryable()
                .DynamicSort(columnToSort)
                .ToList();
        }

        StateHasChanged();
    }

    private static void OnRowStyling(IncidentReader incident, DataGridRowStyling styling)
    {
        var incidentSeverity = (Severity)incident.Severity;

        switch (incidentSeverity)
        {
            case Severity.Minor:
                break;
            case Severity.BlackShirt:
                break;
            case Severity.Captain:
                break;
            case Severity.Admin:
                styling.Background = Background.Danger;
                break;
            case Severity.Adh:
                styling.Background = Background.Danger;
                break;
            case Severity.Dh:
                styling.Background = Background.Danger;
                break;
            case Severity.Police:
                styling.Background = Background.Danger;
                break;
            case Severity.Resolved:
                styling.Background = Background.Info;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private Task Reset()
    {
        return _dataGrid.Reload();
    }
    #endregion

    #region DataGrid Mutation Methods
    private async Task OnRowInserting(CancellableRowChange<IncidentReader, Dictionary<string, object>> newIncident)
    {
        var recordingStaffMember = _staffMembers.First(st => st.Id == _selectedRecordingStaffMember);
        var reportingStaffMember = _staffMembers.First(st => st.Id == _selectedRecordingStaffMember);

        var shiftReportedUnder = _shifts.First(sh => sh.Id == _selectedShift);

        var locationOccurredAt = _locations.First(l => l.Id == (_selectedOccurrenceLocation));

        var addIncidentCommand = new AddIncidentCommandWithReturn
        {
            RecordedById = recordingStaffMember.Id,
            ReportedById = reportingStaffMember.Id,
            Description = newIncident.Values["Description"].ToString(),
            LocationId = locationOccurredAt.Id,
            Severity = (Severity)_selectedSeverity,
            ShiftId = shiftReportedUnder.Id,
            Title = newIncident.Values["Title"].ToString()
        };

        var addedEntityResponse = await IncidentService.AddIncidentAsync(addIncidentCommand);

        if (addedEntityResponse.ResponseCode is not ResponseCodes.ApiSuccess)
        {
            await NotificationService.Error(new MarkupString($"<em>{addedEntityResponse.ResponseMessage}</em>"), "Failed to add incident");

            return;
        }

        newIncident.Item.Id = addedEntityResponse.Data;
        newIncident.Item.ReportedBy = reportingStaffMember;
        newIncident.Item.RecordedBy = recordingStaffMember;
        newIncident.Item.Location = locationOccurredAt;
        newIncident.Item.RecordedOn = _selectedRecordedDate.GetValueOrDefault();

        await NotificationService.Success(new MarkupString($"<em>{addedEntityResponse.ResponseMessage}</em>"),
            "Successfully Added Incident");

        StateHasChanged();
    }

    private void OnRowUpdated(SavedRowItem<ShiftReader, Dictionary<string, object>> e)
    {
        var incident = e.Item;
    }

    private async Task OnResolvingIncident(Guid incidentId)
    {
        _isLoading = true;

        var resolveIncidentCommand = new ResolveIncidentCommandWithReturn(incidentId);

        var resolvedIncidentResponse = await IncidentService.ResolveIncidentAsync(resolveIncidentCommand);

        var markUpResponse = new MarkupString($"<em>{resolvedIncidentResponse.ResponseMessage}</em>");

        if (resolvedIncidentResponse.ResponseCode is not ResponseCodes.ApiSuccess)
        {
            await NotificationService.Error(markUpResponse, "Failed to resolve Incident");

            _isLoading = false;

            return;
        }

        await NotificationService.Success(markUpResponse, $"Resolved Incident Successfully");

        _isLoading = false;
    }

    private async Task OnUpdatingIncidentSeverity(Guid incidentId)
    {
        _isLoading = true;

        var targetSeverity = (Severity)_selectedSeverity;

        var adjustIncidentSeverityCommand = new AdjustIncidentSeverityCommandWithReturn(incidentId, targetSeverity);

        var severityUpdatedResponse = await IncidentService.AdjustIncidentSeverityAsync(adjustIncidentSeverityCommand);

        var markUpResponse = new MarkupString($"<em>{severityUpdatedResponse.ResponseMessage}</em>");

        if (severityUpdatedResponse.ResponseCode is not ResponseCodes.ApiSuccess)
        {
            await NotificationService.Error(markUpResponse, "Failed to update the incident's severity");
            _isLoading = false;
            return;
        }

        await NotificationService.Success(markUpResponse, "Updated incident's severity Successfully");
        _isLoading = false;
    }
    #endregion

    #region Edit Form Methods
    private void ShowModal(Guid incidentId)
    {
        _selectedIncident = _incidents.Single(i => i.Id == incidentId);

        _modalRef.Show();
    }

    private void HideModal()
    {
        _modalRef.Hide();
    }
    #endregion

    #region SelectList Methods
    private void OnRecordedDateChanged(DateTime? date)
    {
        _selectedRecordedDate = date;
    }

    private void OnReportingStaffMemberChanged(Guid value)
    {
        _selectedRecordingStaffMember = value;

        StateHasChanged();
    }

    private void onRecordingStaffMemberChanged(Guid value)
    {
        _selectedRecordingStaffMember = value;

        StateHasChanged();
    }

    private void OnSelectedLocationChanged(Guid value)
    {
        _selectedOccurrenceLocation = value;

        StateHasChanged();
    }
    #endregion
}