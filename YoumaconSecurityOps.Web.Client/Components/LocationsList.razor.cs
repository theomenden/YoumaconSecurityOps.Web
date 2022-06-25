using MediatR;
using YsecOps.Core.Mediator.Requests.Queries;
using YSecOps.Data.EfCore.Models;

namespace YoumaconSecurityOps.Web.Client.Components
{
    public partial class LocationsList : ComponentBase
    {
        [Inject] public IMediator Mediator { get; init; }

        private List<Location> _locations = new(20);

        private Guid _selectedSearchedLocation;

        private String _selectedAutoCompleteText = String.Empty;

        protected override async Task OnInitializedAsync()
        {
            _locations = await Mediator.Send(new GetLocationsQuery());
        }
    }
}
