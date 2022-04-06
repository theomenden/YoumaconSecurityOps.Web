namespace YoumaconSecurityOps.Web.Client.Components;

public partial class StaffEntryForm : ComponentBase
{
    #region Injected Services
    [Inject] public NavigationManager NavigationManager { get; init; }

    [Inject] public IStaffService StaffService { get; init; }

    [Inject] public IContactService ContactService { get; init; }
    #endregion
    #region Instance Members
    private IEnumerable<StaffRole> _staffRoles = new List<StaffRole>(5);

    private IEnumerable<StaffType> _staffTypes = new List<StaffType>(5);

    private IEnumerable<Pronoun> _pronouns = new List<Pronoun>(14);

    private Boolean _isLoading;
    private Boolean _isContactInfoPrepared;
    private Boolean _isStaffInfoPrepared;
    private Boolean _needCrashSpace;
    private Boolean _isBlackShirt;
    private Boolean _isRaveApproved;
    private Boolean _isSavingContactInfo;
    private Boolean _isSavingStaffInfo;
    private Boolean _isSubmitting;

    //Default to GENERAL
    private Int32 _selectedStaffRole = 5;
    
    //Default to FLOOR
    private Int32 _selectedStaffType = 1;

    //Default to ASK
    private Int32 _selectedPronoun;
    private String _phoneNumber;

    private String _firstName = String.Empty;
    private String _lastName = String.Empty;
    private String _preferredName = String.Empty;
    private String _facebookName = String.Empty;
    private String _email = String.Empty;
    private String _shirtSize = String.Empty;
    private string selectedStep = "step1";

    private List<ApiResponse> _apiResponses = new (3);

    private ContactWriter _contactWriter;

    private StaffWriter _staffWriter;
    #endregion

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;

        _staffRoles = await StaffService.GetStaffRolesAsync(new GetStaffRolesQuery());

        _staffTypes = await StaffService.GetStaffTypesAsync(new GetStaffTypesQuery());

        _pronouns = await StaffService.GetPronounsAsync(new GetPronounsQuery());

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

    private void OnSelectedPronounChanged(Int32 pronounId)
    {
        _selectedPronoun = pronounId;

        StateHasChanged();
    }

    private async Task SaveContactInformation()
    {
        _isSavingContactInfo = true;

        var phoneNumberForStorage = Convert.ToInt64(_phoneNumber);

        _contactWriter = new ContactWriter(_staffWriter.Id, _selectedPronoun, DateTime.Now, _email, _firstName, _lastName, _facebookName, _preferredName, phoneNumberForStorage);

        var command = new AddContactCommand(_contactWriter);

        var savedContactResponse = await ContactService.AddContactInformationAsync(command);

        if (savedContactResponse.Outcome.IsError)
        {
            _apiResponses.Add(savedContactResponse);
        }

        _isContactInfoPrepared = savedContactResponse.ResponseCode is ResponseCodes.ApiSuccess;

        _isSavingContactInfo = false;
    }

    private async Task SaveStaffInformation()
    {
        _isSavingStaffInfo = true;

        _staffWriter = new StaffWriter(_selectedStaffRole, _selectedStaffType, _needCrashSpace, _isBlackShirt, _isRaveApproved, _shirtSize);

        var command = new AddStaffCommand(_staffWriter);

        var savedStaffInformationResponse = await StaffService.AddNewStaffMemberAsync(command);
        
        if (savedStaffInformationResponse.Outcome.IsError)
        {
            _apiResponses.Add(savedStaffInformationResponse);
        }

        _isStaffInfoPrepared = savedStaffInformationResponse.ResponseCode is ResponseCodes.ApiSuccess;

        _isSavingStaffInfo = false;
    }

    private async Task OnSubmit()
    {
        _isSubmitting = true;
        
        await SaveStaffInformation();

        await SaveContactInformation();

        var staffRoleMapWriter = new StaffTypeRoleMapWriter(_staffWriter.Id, _selectedStaffType, _selectedStaffRole);

        var addStaffRoleMapCommand = new AddStaffTypeRoleMapCommand(staffRoleMapWriter);

        await StaffService.AddNewStaffTypeRoleMapAsync(addStaffRoleMapCommand);

        _isSubmitting = false;

        NavigationManager.NavigateTo("/staff");
    }

    private Task OnSelectedStepChanged(string name)
    {
        selectedStep = name;

        return Task.CompletedTask;
    }

    private Task OnPreviousButtonClicked()
    {
        if (selectedStep.Equals("step2"))
        {
            selectedStep = "step1";
        }

        if (selectedStep.Equals("step3"))
        {
            selectedStep = "step2";
        }

        return Task.CompletedTask;
    }

    private Task OnNextButtonClicked()
    {
        if (selectedStep.Equals("step2"))
        {
            selectedStep = "step3";
        }

        if (selectedStep.Equals("step1"))
        {
            selectedStep = "step2";
        }
        return Task.CompletedTask;
    }

}

