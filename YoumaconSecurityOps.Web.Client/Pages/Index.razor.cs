using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Index : ComponentBase
{
    private string _selectedSlide = "1";

    private bool _isLoading;

    private readonly IEnumerable<ImageInformationHolder> _images = new List<ImageInformationHolder>(20);

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        
        _isLoading = false;
    }
}
