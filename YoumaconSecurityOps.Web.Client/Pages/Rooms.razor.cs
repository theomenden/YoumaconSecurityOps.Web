namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Rooms : ComponentBase
{
    [Inject]
    public IRoomService RoomService { get; init; }

    [Inject]
    public IStaffService StaffService { get; init; }

    private Int32 _totalRooms = 0;

    private List<RoomScheduleReader> _gridDisplay = new(20);

    private RoomScheduleReader _selectedRoom;

    private DataGrid<RoomScheduleReader> _dataGrid = new();

    private async Task LoadRooms(CancellationToken cancellationToken = default)
    {
        _gridDisplay = await RoomService.GetRoomScheduleAsync(new GetRoomScheduleQuery(), cancellationToken);

        await StaffService.GetStaffMembersWithResponseAsync(new GetStaffQuery(), cancellationToken);
    }

    private static void OnRowStyling(RoomScheduleReader room, DataGridRowStyling styling)
    {

        if (room is not null && room.IsCurrentlyOccupied)
        {
            styling.Background = Background.Warning;
        }
    }

    private Task Reset()
    {
        return _dataGrid.Reload();
    }

    private static String DetermineDisplayIcon(Boolean statusCheck)
    {
        return statusCheck ? " fa-check-circle text-success" : " fa-times-circle text-danger";
    }

    private static string SetPopupTitle(PopupTitleContext<RoomScheduleReader> context)
    {
        var popupTitle = context.LocalizationString + " Room ";

        if (context.EditState is DataGridEditState.Edit)
        {
            popupTitle += context.Item.RoomNumber;
        }

        return popupTitle;
    }


    private async Task OnReadData(DataGridReadDataEventArgs<RoomScheduleReader> e)
    {
        await LoadRooms(e.CancellationToken);

        if (!e.CancellationToken.IsCancellationRequested)
        {
            var sortDeterminant = new DataGridHelpers<RoomScheduleReader>(e);

            var columnToSort =
                sortDeterminant.ColumnStates.Where(cs =>
                    cs.SortDirection is not SortDirection.Default).ToList();

            _totalRooms = _gridDisplay.Count;

            _gridDisplay = _gridDisplay
                .DynamicFilter(columnToSort)
                .DynamicSort(columnToSort)
                .ToList();
        }

        StateHasChanged();
    }
}