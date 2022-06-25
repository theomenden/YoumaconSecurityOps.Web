using MediatR;
using YsecOps.Core.Mediator.Requests.Queries;
using YsecOps.Core.Mediator.Requests.Queries.Streaming;
using YSecOps.Data.EfCore.Models;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class StaffList : ComponentBase
{
    [Inject] IMediator Mediator { get; init; }

    private List<Staff> _members = new(200);

    private Int32 _totalMembers;

    private Task<Int32> GetTotalStaffMembersAsync(CancellationToken cancellationToken) =>
        Mediator.Send(new GetCountOfStaffMembersQuery(), cancellationToken);

    private async Task LoadStaffMemberData(CancellationToken cancellationToken = default)
    {
        _members = await Mediator.CreateStream(new GetStaffMembersQuery(), cancellationToken).ToListAsync(cancellationToken);
    }

    private async Task OnReadData(DataGridReadDataEventArgs<Staff> eventArgs)
    {
        if (!eventArgs.CancellationToken.IsCancellationRequested)
        {
            _totalMembers = await GetTotalStaffMembersAsync(eventArgs.CancellationToken);

            await LoadStaffMemberData(eventArgs.CancellationToken);
        }
    }

    private static void OnRowStyling(Staff member, DataGridRowStyling dataGridRowStyling)
    {
        if (!member.IsOnBreak)
        {
            return;
        }

        dataGridRowStyling.Class = String.Empty;
        dataGridRowStyling.Background = Background.Danger;
        dataGridRowStyling.Color = Color.Light;
        dataGridRowStyling.Class += "fw-bolder text-uppercase text-decoration-line-through";

    }

}
