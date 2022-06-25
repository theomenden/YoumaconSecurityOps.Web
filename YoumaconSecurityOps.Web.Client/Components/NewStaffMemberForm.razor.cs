using MediatR;
using YsecOps.Core.Mediator.Requests.Queries;
using YSecOps.Data.EfCore.Models;

namespace YoumaconSecurityOps.Web.Client.Components;

public partial class NewStaffMemberForm : ComponentBase
{
    [Inject] public IMediator Mediator { get; init; }

    private List<Pronoun> _pronouns = new (20);

    private List<StaffType> _staffTypes = new(10);
    private List<Role> _staffRoles = new(10);

    #region Form Fields
    private Int32 _pronounId = 14;
    private Int32 _roleId = 5;
    private Int32 _staffTypeId = 1;
    private bool _needsCrashSpace;
    private bool _isBlackShirt;
    private bool _isRaveApproved;
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _phoneNumber;
    private string _preferredName;

    #endregion
    protected override async Task OnInitializedAsync()
    {
        _pronouns = await Mediator.Send(new GetPronounsQuery());

        _staffTypes = await Mediator.Send(new GetStaffTypesQuery());

        _staffRoles = await Mediator.Send(new GetStaffRolesQuery());
    }
}

