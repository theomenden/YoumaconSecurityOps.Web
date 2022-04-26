using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Index : ComponentBase
{
    [Inject] public IMediator Mediator { get; init; }

    private string _selectedSlide = "1";

    private bool _isLoading;

    private readonly IList<ShiftReader> _shiftsForStaffMember = new List<ShiftReader>(20);

    private readonly IEnumerable<ImageInformationHolder> _images = new List<ImageInformationHolder>(20);

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;

        if (Guid.TryParse("854F9244-ED91-410D-899D-EEFF45B4BBA9", out var staffId))
        {
            var queryParams = new ShiftQueryStringParameters(new[] { staffId }, null, null);

            await foreach (var shift in Mediator.CreateStream(new GetShiftListWithParametersQuery(queryParams)))
            {
                _shiftsForStaffMember.Add(shift);
            }
        }

        _isLoading = false;
    }
}
