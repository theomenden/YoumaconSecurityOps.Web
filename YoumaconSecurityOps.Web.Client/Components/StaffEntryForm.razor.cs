namespace YoumaconSecurityOps.Web.Client.Components;

public partial class StaffEntryForm : ComponentBase
{
    [Inject] public IStaffService StaffService { get; set; }

    private IEnumerable<StaffRole> _staffRoles = new List<StaffRole>(5);

    private IEnumerable<StaffType> _staffTypes = new List<StaffType>(5);

    private Boolean _isLoading;
    private Boolean _isContactInfoPrepared;
    private Boolean _isStaffInfoPrepared;
    private Boolean _needCrashSpace;
    private Boolean _isBlackShirt;
    private Boolean _isRaveApproved;


    private Int32 _selectedStaffRole;
    private Int32 _selectedStaffType;

    private String _firstName = String.Empty;
    private String _lastName = String.Empty;
    private String _preferredName = String.Empty;
    private String _facebookName = String.Empty;
    private String _email = String.Empty;
    private String _phoneNumber = String.Empty;
    private String _shirtSize = String.Empty;

    private ContactWriter _contactWriter;

    private StaffWriter _staffWriter;

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;

        _staffRoles = await StaffService.GetStaffRolesAsync(new GetStaffRolesQuery());

        _staffTypes = await StaffService.GetStaffTypesAsync(new GetStaffTypesQuery());

        _isLoading = false;
    }

    private void OnSelectedStaffRoleChanged(Int32 staffRole)
    {
        _selectedStaffRole = staffRole;

        StateHasChanged();
    }

    private void OnSelectedStaffTypeChanged(Int32 staffType)
    {
        _selectedStaffType = staffType;

        StateHasChanged();
    }

    private Task SaveContactInformation()
    {
        _contactWriter = new ContactWriter(_staffWriter.Id, DateTime.Now, _email, _firstName, _lastName, _facebookName, _preferredName, Convert.ToInt64(_phoneNumber));

        _isContactInfoPrepared = true;

        return Task.CompletedTask;
    }

    private Task SaveStaffInformation()
    {
        _staffWriter = new StaffWriter(_selectedStaffRole, _selectedStaffType, _needCrashSpace, _isBlackShirt, _isRaveApproved, _shirtSize);

        _isStaffInfoPrepared = true;

        return Task.CompletedTask;
    }

    private async Task OnSubmit()
    {
        if (_staffWriter is not null && _contactWriter is not null)
        {
            var fullStaffAddCommand = new AddFullStaffEntryCommand(_staffWriter, _contactWriter);

            await StaffService.AddNewStaffMemberAsync(fullStaffAddCommand);
        }
    }

    private bool IsDisabled() => (_isContactInfoPrepared && _isStaffInfoPrepared) is not true;
}

