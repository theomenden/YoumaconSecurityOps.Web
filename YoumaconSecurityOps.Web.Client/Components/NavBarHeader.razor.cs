using System.Globalization;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Localization;
using Microsoft.AspNetCore.Components;

namespace YoumaconSecurityOps.Web.Client.Components;

public partial class NavBarHeader : ComponentBase
{
    #region Parameters
    [Inject] public ITextLocalizerService LocalizationService { get; init; }
    
    [Parameter] public EventCallback<bool> ThemeEnabledChanged { get; set; }

    [Parameter] public EventCallback<bool> ThemeGradientChanged { get; set; }

    [Parameter] public EventCallback<bool> ThemeRoundedChanged { get; set; }

    [Parameter] public EventCallback<string> ThemeColorChanged { get; set; }

    [CascadingParameter] protected Theme Theme { get; set; }
    #endregion
    #region Fields
    private bool _isTopbarVisible = false;

    private ThemeChoice _selectedTheme;

    private CultureInfo _selectedCultureName;
    
    #endregion
    protected override void OnInitialized()
    {
        _selectedCultureName = LocalizationService.SelectedCulture;

        SelectedCultureChanged(CultureInfo.CurrentCulture);
    }

    private void SelectedCultureChanged(CultureInfo cultureInfo)
    {
        _selectedCultureName = cultureInfo;

        LocalizationService.ChangeLanguage(cultureInfo.Name);

        StateHasChanged();
    }
}