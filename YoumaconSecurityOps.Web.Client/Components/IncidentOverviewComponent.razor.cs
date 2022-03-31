namespace YoumaconSecurityOps.Web.Client.Components;

public partial class IncidentOverviewComponent : ComponentBase
{
    [Parameter] public IEnumerable<IncidentReader> Incidents { get; set; }

    [Inject] public IIncidentService IncidentService { get; init; }

    protected override async Task OnParametersSetAsync()
    {
        Incidents ??= await IncidentService.GetIncidentsAsync(new GetIncidentsQuery());
    }

    private static Color GetBadgeColor(Severity severity) =>
        severity switch
        {
            Severity.Adh => Color.Primary,
            Severity.Admin => Color.Primary,
            Severity.BlackShirt => Color.Warning,
            Severity.Captain => Color.Warning,
            Severity.Minor => Color.Secondary,
            Severity.Dh => Color.Primary,
            Severity.Police => Color.Danger,
            Severity.Resolved => Color.Success,
            _ => throw new ArgumentOutOfRangeException(nameof(severity))
        };
}