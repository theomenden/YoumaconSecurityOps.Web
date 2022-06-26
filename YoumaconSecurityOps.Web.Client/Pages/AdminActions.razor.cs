using YsecOps.Core.Mediator.Requests.Queries.Streaming;

namespace YoumaconSecurityOps.Web.Client.Pages;
public partial class AdminActions : ComponentBase
{
    [Inject] public IMediator Mediator { get; init; }

    private List<Location> _locations = new (15);

    private List<Staff> _members = new (15);

    private List<Shift> _shifts = new(200);

    protected override async Task OnInitializedAsync()
    {
        _locations = await Mediator.Send(new GetLocationsQuery());

        _members = await Mediator.CreateStream(new GetStaffMembersQuery()).ToListAsync();
    }

    private Int32 GetTotalStaffAtLocation(Guid locationId)
    {
        return _shifts.Count(shift => shift.CurrentLocationId == locationId);
    }
}
