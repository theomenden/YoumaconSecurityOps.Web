namespace YoumaconSecurityOps.Web.Client.Components;

public partial class PieChartComponent : ComponentBase
{
    [Parameter] public IEnumerable<ShiftReader> Shifts { get; set; }
    
    [Inject] public IShiftService ShiftService { get; init; }

    private PieChart<PieChartDataModel> _pieChartRef;

    private IEnumerable<String> _labels = new List<string>(10);

    private IEnumerable<PieChartDataModel> _pieChartDataModels = new List<PieChartDataModel>(20);

    private PieChartOptions _pieChartOptions = new()
    {
        Parsing = new ChartParsing
        {
            XAxisKey = "location",
            YAxisKey = "shifts"
        }
    };

    private List<string> _backgroundColors = new() { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
    private List<string> _borderColors = new() { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

    private int pieChartLabel = 0;
    
    private bool _isAlreadyInitialised;

    protected override async Task OnParametersSetAsync()
    {
        if (Shifts?.Count() == 0)
        {
            Shifts = await ShiftService.GetShiftsAsync(new GetShiftListQuery());
        }
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!_isAlreadyInitialised)
        {
            _isAlreadyInitialised = true;


            _pieChartDataModels = Shifts.GroupBy(shiftLocation => shiftLocation.CurrentLocation, shift => shift)
                .Select(shiftGrouping => new PieChartDataModel
                {
                    Location = shiftGrouping.Key.Name,
                    Shifts = shiftGrouping.Count()
                })
                .ToList();

            _labels = _pieChartDataModels.Select(l => l.Location).ToList();

            await HandleChartRedraw(_pieChartRef, GetPieChartDataset);
        }
    }

    private async Task HandleChartRedraw<TDataSet, TItem, TOptions, TModel>(Blazorise.Charts.BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet)
        where TDataSet : ChartDataset<TItem>
        where TOptions : ChartOptions
        where TModel : ChartModel
    {
        await chart.Clear();

        await chart.AddLabelsDatasetsAndUpdate(_labels.ToList().AsReadOnly(), getDataSet());
    }

    private PieChartDataset<PieChartDataModel> GetPieChartDataset()
    {
        return new()
        {
            Label = "Percentage of shifts at locations",
            Data = _pieChartDataModels.ToList(),
            BackgroundColor = _backgroundColors,
            BorderColor = _borderColors,
            BorderWidth = 1
        };
    }
}

