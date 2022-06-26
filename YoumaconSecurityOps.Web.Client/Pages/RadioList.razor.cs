using YsecOps.Core.Mediator.Requests.Queries.Streaming;

namespace YoumaconSecurityOps.Web.Client.Pages;
public partial class RadioList : ComponentBase
{
    [Inject] public IMediator Mediator { get; init; }

    private List<RadioSchedule> _radioScheduleList = new(20);

    private RadioSchedule? _selectedRadio;

    private Int32 _totalRadios;
    private Int32 _checkedOutRadios;
    private Int32 _chargingRadios;

    private async Task GetTotalRadiosAsync(CancellationToken cancellationToken)
    {
        _totalRadios = await Mediator.Send(new GetTotalRadiosQuery(), cancellationToken);
    }

    private async Task GetTotalCheckedOutRadiosAsync(CancellationToken cancellationToken)
    {
        _checkedOutRadios = await Mediator.Send(new GetCheckedOutRadiosCountQuery(), cancellationToken);
    }

    private async Task GetTotalChargingRadiosAsync(CancellationToken cancellationToken)
    {
        _chargingRadios = await Mediator.Send(new GetChargingRadiosCountQuery(), cancellationToken);
    }
    private async Task LoadRadioData(CancellationToken cancellationToken = default)
    {
        var getAggregateData = new[]
        {
            GetTotalRadiosAsync(cancellationToken),
            GetTotalCheckedOutRadiosAsync(cancellationToken),
            GetTotalChargingRadiosAsync(cancellationToken)
        };

        await Task.WhenAll(getAggregateData);

        _radioScheduleList = await Mediator.CreateStream(new GetAllRadiosQuery(), cancellationToken).ToListAsync(cancellationToken);
    }

    private async Task OnReadData(DataGridReadDataEventArgs<RadioSchedule> eventArgs)
    {
        if (!eventArgs.CancellationToken.IsCancellationRequested)
        {
            await LoadRadioData(eventArgs.CancellationToken);
        }
    }

    private string GetMemberName(RadioSchedule radioSchedule)
    {
        var staffHoldingRadio = radioSchedule.LastStaffToHave;

        var contactInformation = staffHoldingRadio.Contacts.FirstOrDefault();

        return $"({contactInformation?.Pronoun.Name}){contactInformation?.PreferredName} {contactInformation?.LastName}";

    }
}
