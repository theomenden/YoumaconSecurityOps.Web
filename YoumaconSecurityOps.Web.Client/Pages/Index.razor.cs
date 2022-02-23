namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Index : ComponentBase
{
    // [Inject] private IDirectoryReadingService DirectoryReadingService { get; set; }

    private string _selectedSlide = "1";

    private IEnumerable<ImageInformationHolder> _images = new List<ImageInformationHolder>(20);
}
