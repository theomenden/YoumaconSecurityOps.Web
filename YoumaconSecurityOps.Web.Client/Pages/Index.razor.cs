using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    public IShiftService ShiftService { get; init; }

    private string _selectedSlide = "1";

    private bool _isLoading;

    private IEnumerable<ShiftReader> _shiftsForStaffMember = new List<ShiftReader>(20);

    private readonly IEnumerable<ImageInformationHolder> _images = new List<ImageInformationHolder>(20);

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;

        var isSuccess = Guid.TryParse("2618F3A4-019E-4E91-A115-4A1E26D9AD18", out var staffId);

        var queryParams = new ShiftQueryStringParameters(new[] { staffId }, null, null);

        _shiftsForStaffMember = await ShiftService.GetShiftsAsync(new GetShiftListWithParametersQuery(queryParams));

        _isLoading = false;
    }
}
