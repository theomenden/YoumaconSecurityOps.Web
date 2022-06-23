using MediatR;
using YoumaconSecurityOps.Core.Shared.Parameters;
using YsecOps.Core.Mediator.Requests.Queries;
using YSecOps.Data.EfCore.Models;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class Index : ComponentBase
{

    [Inject] public IMediator Mediator { get; init; }


    private string _selectedSlide = "1";

    private bool _isLoading;

    private List<Pronoun> _pronouns = new(15);

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;

        _pronouns = await Mediator.Send(new GetPronounsQuery());

        _isLoading = false;
    }
}
