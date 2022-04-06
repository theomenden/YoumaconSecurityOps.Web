using System.Threading.Tasks.Dataflow;
using YoumaconSecurityOps.Shared.Models.Procedures;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Admin : ComponentBase
{
    #region Injected Services

    [Inject] public ILocationService LocationService { get; init; }

    [Inject] public IShiftService ShiftService { get; init; }

    [Inject] public IStaffService StaffService { get; init; }

    [Inject] public IIncidentService IncidentService { get; init; }

    [Inject] public INotificationService NotificationService { get; init; }
    #endregion
    #region Private Members
    private IEnumerable<LocationReader> _locations = new List<LocationReader>(20);

    private IEnumerable<ShiftReader> _shifts = new List<ShiftReader>(50);

    private IEnumerable<StaffReader> _staff = new List<StaffReader>(50);

    private IEnumerable<DropItem> _dropItems = new List<DropItem>(50);

    private IEnumerable<IncidentReader> _incidents = new List<IncidentReader>(50);

    private Guid _selectedSearchValue = Guid.Empty;

    private string _selectedAutoCompleteText = String.Empty;

    private Chart<Double> _pieChartRef;

    private LineChart<LiveDataPoint> _liveUpdateLineChart;

    private IEnumerable<String> _labels = new List<string>(10);

    private IEnumerable<PieChartDataModel> _pieChartDataModels = new List<PieChartDataModel>(20);

    private readonly PieChartOptions _pieChartOptions = new()
    {
        AspectRatio = 1.5,
        Parsing = new ChartParsing
        {
            XAxisKey = "location",
            YAxisKey = "shifts"
        }
    };

    private readonly LineChartOptions _liveUpdateChartOptions = new()
    {
        AspectRatio = 1.5,
        Scales = new()
        {
            X = new()
            {
                Min = 0
            },
            Y = new()
            {
                Min = 0
            }
        }
    };


        private List<string> _backgroundColors = new() { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
    private List<string> _borderColors = new() { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

    private bool _isAlreadyInitialised;

    #endregion
    #region Lifecycle Methods
    protected override async Task OnInitializedAsync()
    {
        await LoadInitialData();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!_isAlreadyInitialised)
        {
            _isAlreadyInitialised = true;

            _pieChartDataModels = _shifts
                .GroupBy(shiftLocation => shiftLocation.CurrentLocation, shift => shift)
                .Select(shiftGrouping => new PieChartDataModel
                {
                    Location = shiftGrouping.Key.Name,
                    Shifts = shiftGrouping.Count()
                })
                .ToList();

            _labels = _pieChartDataModels.Select(l => l.Location);

            await Task.WhenAll(
                HandleChartRedraw(_pieChartRef, GetPieChartDataset),
                HandleChartRedraw(_liveUpdateLineChart, GetUpdatedLineChartDataset));
        }
    }
    #endregion

    #region Chart Drawing Methods
    private async Task HandleChartRedraw<TDataSet, TItem, TOptions, TModel>(BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet)
        where TDataSet : ChartDataset<TItem>
        where TOptions : ChartOptions
        where TModel : ChartModel
    {
        await chart.Clear();

        var labelsAsReadOnly = _labels
            .ToList()
            .AsReadOnly();

        await chart.AddLabelsDatasetsAndUpdate(labelsAsReadOnly, getDataSet());
    }

    private PieChartDataset<Double> GetPieChartDataset()
    {
        return new()
        {
            Label = "Shifts at their locations",
            Data = _pieChartDataModels.Select(pd => pd.Shifts).ToList(),
            BackgroundColor = _backgroundColors,
            BorderColor = _borderColors,
            BorderWidth = 1
        };
    }
    
    private Task OnLiveUpdateChartRefreshed(ChartStreamingData<LiveDataPoint> data)
    {
        data.Value = new()
        {
            X = DateTime.Now,
            Y = _staff.Count(s => s.IsOnBreak),
        };

        return Task.CompletedTask;
    }

    private LineChartDataset<LiveDataPoint> GetUpdatedLineChartDataset()
    {
        return new()
        {
            Data = new(),
            Label = "Dataset 1 (linear interpolation)",
            BackgroundColor = _backgroundColors[0],
            BorderColor = _borderColors[0],
            Fill = false,
            Tension = 0,
            CubicInterpolationMode = "monotone",
            BorderDash = new() { 8, 4 }
        };
    }
    #endregion
    private async Task OnItemDropped(DraggableDroppedEventArgs<DropItem> e)
    {

        var updatedLocation = _locations.FirstOrDefault(l => l.Name.Equals(e.DropZoneName))?.Id ?? e.Item.LocationId;

        if (e.Item.ShiftId != Guid.Empty)
        {
            var command = new UpdateShiftLocationCommandWithReturn(e.Item.ShiftId, updatedLocation);

            e.Item.LocationId = updatedLocation;

            await ShiftService.UpdateShiftLocationAsync(command);
        }

        if (e.Item.ShiftId == Guid.Empty)
        {
            var startTime = DateTime.Now.AddMinutes(15);

            var endTime = startTime.AddHours(2);

            var command = new AddShiftCommandWithReturn(startTime, endTime, e.Item.StaffId, e.Item.MemberName, updatedLocation);

            await ShiftService.AddShiftAsync(command);
        }

        await NotificationService.Info($"Updated Location for {e.Item.MemberName} to {e.DropZoneName} from {e.Item.LocationName}", "Location Updated");

        _isAlreadyInitialised = false;

        await LoadInitialData();

        StateHasChanged();
    }

    private async Task LoadInitialData()
    {
        _shifts = await ShiftService.GetShiftsAsync(new GetShiftListQuery());

        _staff = await StaffService.GetStaffMembersAsync(new GetStaffQuery());

        _incidents = await IncidentService.GetIncidentsAsync(new GetIncidentsQuery());

        _dropItems = _staff
            .GroupJoin(_shifts,
                member => member.Id,
                shift => shift.StaffId,
                (member, gj) => new { member, gj })
            .SelectMany(groupResult => groupResult.gj.DefaultIfEmpty(),
                (groupResult, shift) => new DropItem
                {
                    LocationId = shift?.CurrentLocationId ?? Guid.Empty,
                    LocationName = shift?.CurrentLocation?.Name ?? "Awaiting Assignment",
                    StaffId = groupResult.member.Id,
                    MemberName = groupResult.member.ContactInformation.PreferredName,
                    ShiftId = shift?.Id ?? Guid.Empty
                });

        _locations = await LocationService.GetLocationsAsync(new GetLocationsQuery());
    }
}

