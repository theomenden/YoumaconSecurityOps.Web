using System;
using System.Globalization;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Localization;
using Microsoft.AspNetCore.Components;

namespace YoumaconSecurityOps.Web.Client.Shared
{
    public partial class MainLayout: LayoutComponentBase
    {
        [Inject] public ITextLocalizerService LocalizationService { get; set; }

        [Inject] public IPageProgressService PageProgressService { get; set; }

        [CascadingParameter] protected Theme Theme { get; set; }

        protected override async Task OnInitializedAsync()
        {
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

    }
}
