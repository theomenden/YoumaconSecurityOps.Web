using System;
using System.Globalization;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;
using YoumaconSecurityOps.Web.Client.Middleware;
using YoumaconSecurityOps.Web.Client.Models;

namespace YoumaconSecurityOps.Web.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        [Inject] public CircuitHandler CircuitHandler { get; set; }

        [Inject] public SessionDetails SessionData { get; set; }

        [Inject] public ITextLocalizerService LocalizationService { get; set; }

        [Inject] public IPageProgressService PageProgressService { get; set; }

        [CascadingParameter] protected Theme Theme { get; set; }

        private readonly SessionModel _sessionModel = new ();

        protected override async Task OnInitializedAsync()
        {
            (CircuitHandler as TrackingCircuitHandler).CircuitsChanged += HandleCircuitsChanged;

            var circuitId = (CircuitHandler as TrackingCircuitHandler).CircuitId;

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

            //if ( Theme.GradientOptions == null )
            //    Theme.GradientOptions = new GradientOptions();

            //Theme.GradientOptions.BlendPercentage = 80;

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

        private Task OnThemeColorChanged(string value)
        {
            if (Theme is null)
            {
                return Task.CompletedTask;
            }

            Theme.ColorOptions ??= new ThemeColorOptions();

            Theme.BackgroundOptions ??= new ThemeBackgroundOptions();

            Theme.TextColorOptions ??= new ThemeTextColorOptions();

            Theme.ColorOptions.Primary = value;
            Theme.BackgroundOptions.Primary = value;
            Theme.TextColorOptions.Primary = value;

            Theme.InputOptions ??= new ThemeInputOptions();

            //Theme.InputOptions.Color = value;
            Theme.InputOptions.CheckColor = value;
            Theme.InputOptions.SliderColor = value;

            Theme.SpinKitOptions ??= new();

            Theme.SpinKitOptions.Color = value;

            Theme.ThemeHasChanged();

            return Task.CompletedTask;
        }

        private void HandleCircuitsChanged(object sender, EventArgs args)
        {
           InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            (CircuitHandler as TrackingCircuitHandler).CircuitsChanged -= HandleCircuitsChanged;
            GC.SuppressFinalize(this);
        }
    }
}
