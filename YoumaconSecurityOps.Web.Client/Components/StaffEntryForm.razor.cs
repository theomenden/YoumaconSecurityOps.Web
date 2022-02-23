namespace YoumaconSecurityOps.Web.Client.Components;

    public partial class StaffEntryForm : ComponentBase
    {
        [Inject] public IStaffService StaffService { get; set; }

        [Inject] public IMediator Mediator { get; set; }

        private IEnumerable<StaffRole> _staffRoles = new List<StaffRole>(5);

        private IEnumerable<StaffType> _staffTypes = new List<StaffType>(5);


        private Boolean _isLoading = false;

        private Int32 _selectedStaffRole;

        private Int32 _selectedStaffType;

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
            _contactWriter = new ContactWriter(DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, 0L);

            return Task.CompletedTask;
        }

        private Task SaveStaffInformation()
        {
            _staffWriter = new StaffWriter(_contactWriter.Id, 1, 1, false, false, false, string.Empty);

            return Task.CompletedTask;
        }

        private async Task OnSubmit()
        {
            await Mediator.Send(new AddFullStaffEntryCommand(_staffWriter, _contactWriter));
        }
    }

