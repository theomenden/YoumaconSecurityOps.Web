using MediatR;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Index : ComponentBase
{
    [Inject] public IMediator Mediator { get; init; }

    private string _selectedSlide = "1";
}
