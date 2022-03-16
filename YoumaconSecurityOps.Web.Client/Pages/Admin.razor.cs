using YoumaconSecurityOps.Shared.Models.Procedures;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Admin : ComponentBase
{
    [Inject] public ILocationService LocationService { get; init; }

    [Inject] public IShiftService ShiftService { get; init; }

    [Inject] private IMediator Mediator { get; init; }

    [Inject] public IStaffService StaffService { get; init; }

    private IEnumerable<LocationReader> _locations = new List<LocationReader>(20);

    private IEnumerable<ShiftReader> _shifts = new List<ShiftReader>(50);
    
    private IEnumerable<StaffReader> _staff = new List<StaffReader>(50);

    protected override async Task OnInitializedAsync()
    {
        await LoadInitialData();
    }

    private async Task OnItemDropped(DraggableDroppedEventArgs<StaffReader> e)
    {
       var staffId = e.Item.Id;

       var mostRecentShift = _shifts.Where(sh => sh.Id == staffId).Max();

       var selectedLocation = _locations.FirstOrDefault(l => l.Name.Equals(e.DropZoneName));

       var command = new UpdateShiftLocationCommandWithReturn(mostRecentShift.Id, selectedLocation.Id);

       await Mediator.Send(command);
    }

    private bool OnFilterItems(ShiftReader item, String dropZone)
    {
        return item.CurrentLocation.Name?.Equals(dropZone) ?? false;
    }

    private async Task LoadInitialData()
    {

        _shifts = await ShiftService.GetShiftsAsync(new GetShiftListQuery());

        _staff = await StaffService.GetStaffMembersAsync(new GetStaffQuery());

        _locations = await LocationService.GetLocationsAsync(new GetLocationsQuery());
    }
}

