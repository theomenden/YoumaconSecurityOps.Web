using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Web.Client.Models;
using YoumaconSecurityOps.Web.Client.Services;

namespace YoumaconSecurityOps.Web.Client.Pages
{
    public partial class Index: ComponentBase
    {
       // [Inject] private IDirectoryReadingService DirectoryReadingService { get; set; }

        private string _selectedSlide = "1";

        private IEnumerable<ImageInformationHolder> _images = new List<ImageInformationHolder>(20);

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
