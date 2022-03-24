using System.Globalization;
using Blazorise.Localization;
using Microsoft.AspNetCore.Components.Web;

namespace YoumaconSecurityOps.Web.Client.Shared;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject] public CircuitHandler CircuitHandler { get; init; }

    [Inject] public SessionDetails SessionData { get; init; }

    [Inject] public ITextLocalizerService LocalizationService { get; init; }

    [Inject] public IPageProgressService PageProgressService { get; init; }

    [CascadingParameter] protected Theme Theme { get; set; }

    private readonly SessionModel _sessionModel = new();
    
    private ErrorBoundary? _errorBoundary;

    protected override void OnParametersSet()
    {
        _errorBoundary?.Recover();
    }

    protected override async Task OnInitializedAsync()
    {
        var circuitId = String.Empty;

        if (CircuitHandler is TrackingCircuitHandler circuitHandler)
        {
            circuitHandler.CircuitsChanged += HandleCircuitsChanged;
            circuitId = circuitHandler?.CircuitId;
        }

        _sessionModel.CircuitId = circuitId;

        SessionData.Add(_sessionModel);

        await SelectCulture(CultureInfo.CurrentCulture.Name);

        await base.OnInitializedAsync();
    }


    private Task SelectCulture(string name)
    {
        LocalizationService.ChangeLanguage(name);

        return Task.CompletedTask;
    }

    private Task OnThemeEnabledChanged(bool value)
    {
        if (Theme is null)
        {
            return Task.CompletedTask;
        }

        Theme.Enabled = value;

        Theme.ThemeHasChanged();

        return Task.CompletedTask;
    }

    private Task OnThemeGradientChanged(bool value)
    {
        if (Theme is null)
        {
            return Task.CompletedTask;
        }

        Theme.IsGradient = value;

        Theme.ThemeHasChanged();

        return Task.CompletedTask;
    }

    private Task OnThemeRoundedChanged(bool value)
    {
        if (Theme is null)
        {
            return Task.CompletedTask;
        }

        Theme.IsRounded = value;

        Theme.ThemeHasChanged();

        return Task.CompletedTask;
    }

    private Task OnThemeColorChanged(String value)
    {
        var themeToUse = ThemeChoice
                             .GetAll()
                             .FirstOrDefault(t => t.Name.Equals(value))
            ?? ThemeChoice.Dark;

        Theme = themeToUse.Theme;

        Theme.ThemeHasChanged();

        return Task.CompletedTask;
    }

    private void HandleCircuitsChanged(object sender, EventArgs args)
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        if (CircuitHandler is TrackingCircuitHandler circuitHandler)
        {
            circuitHandler.CircuitsChanged -= HandleCircuitsChanged;
        }

        GC.SuppressFinalize(this);
    }
}