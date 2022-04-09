using System.ComponentModel.DataAnnotations;
using System.Text;

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

    private Task SaveContactInformation()
    {
        var phoneInputAsSpan = _phoneNumber.AsSpan();

         var sb = new StringBuilder();

         foreach (var t in phoneInputAsSpan)
         {
             if (Char.IsDigit(t))
             {
                 sb.Append(t);
             }
         }

        var phoneNumberForStorage = Convert.ToInt64(sb.ToString());

        _preferredName = String.IsNullOrWhiteSpace(_preferredName) ? _firstName : _preferredName;

        _contactWriter = new ContactWriter(_staffWriter.Id, _selectedPronoun, DateTime.Now, _email, _firstName, _lastName, _facebookName, _preferredName, phoneNumberForStorage);
        
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
        _isSubmitting = true;

        await SaveStaffInformation();

        await SaveContactInformation();

        var staffRoleMapWriter = new StaffTypeRoleMapWriter(_staffWriter.Id, _selectedStaffType, _selectedStaffRole);

        var addFullStaffCommand = new AddFullStaffEntryCommandWithReturn(_staffWriter, _contactWriter, staffRoleMapWriter);

        await StaffService.AddNewStaffMemberAsync(addFullStaffCommand);

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

