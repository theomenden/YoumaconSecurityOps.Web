using System.Threading.Tasks.Dataflow;
using YoumaconSecurityOps.Shared.Models.Procedures;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Admin : ComponentBase
{
    [Inject] public ILocationService LocationService { get; init; }

    [Inject] public IShiftService ShiftService { get; init; }
    
    [Inject] public IStaffService StaffService { get; init; }

    [Inject] public INotificationService NotificationService { get; init; }

    private IEnumerable<LocationReader> _locations = new List<LocationReader>(20);

    private IEnumerable<ShiftReader> _shifts = new List<ShiftReader>(50);

    private IEnumerable<StaffReader> _staff = new List<StaffReader>(50);

    private IEnumerable<DropItem> _dropItems = new List<DropItem>(50);

    protected override async Task OnInitializedAsync()
    {
        await LoadInitialData();
    }

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

        await LoadInitialData();

        StateHasChanged();
    }

    private async Task LoadInitialData()
    {
        _shifts = await ShiftService.GetShiftsAsync(new GetShiftListQuery());

        _staff = await StaffService.GetStaffMembersAsync(new GetStaffQuery());

        _dropItems =
            from member in _staff
            join shift in _shifts on member.Id equals shift.StaffId into gj
            from s in gj.DefaultIfEmpty() 
            select new DropItem
            {
                LocationId = s?.CurrentLocationId ?? Guid.Empty,
                LocationName = s?.CurrentLocation?.Name ?? "Awaiting Assignment",
                StaffId = member.Id,
                MemberName = member.Contact.PreferredName,
                ShiftId = s?.Id ?? Guid.Empty
            };

        _locations = await LocationService.GetLocationsAsync(new GetLocationsQuery());
    }
}

