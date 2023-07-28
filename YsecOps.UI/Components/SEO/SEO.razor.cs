using Microsoft.AspNetCore.Components;

namespace YsecOps.UI.Components.SEO;

public partial class SEO : ComponentBase
{
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
    [Parameter] public string Canonical { get; set; }
    [Parameter] public string ImageUrl { get; set; }

    [Inject] private NavigationManager NavigationManager { get; init; }

    private string _url = String.Empty;

    protected override void OnInitialized()
    {
        _url = NavigationManager.ToAbsoluteUri(Canonical).AbsoluteUri;

        ImageUrl = String.IsNullOrEmpty(ImageUrl)
            ? NavigationManager.ToAbsoluteUri("images/the-omen-den-logo.jpg").AbsoluteUri
            : NavigationManager.ToAbsoluteUri(ImageUrl).AbsoluteUri;

        base.OnInitialized();
    }
}
