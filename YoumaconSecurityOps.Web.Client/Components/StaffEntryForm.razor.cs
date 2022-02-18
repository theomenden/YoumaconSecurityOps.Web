using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Models.Writers;
using YoumaconSecurityOps.Web.Client.Services;

namespace YoumaconSecurityOps.Web.Client.Components
{
    public partial class StaffEntryForm : ComponentBase
    {
        [Inject] public IStaffService StaffService { get; set; }

        [Inject] public IMediator Mediatr { get; set; }

        private IEnumerable<StaffRole> _staffRoles = new List<StaffRole>(5);

        private IEnumerable<StaffType> _staffTypes = new List<StaffType>(5);


        private Boolean _isLoading = false;

        private Int32 _selectedStaffRole;

        private Int32 _selectedStaffType;

        private ContactWriter _contactWriter;

        private StaffWriter _staffWriter;

        protected override async Task OnInitializedAsync()
        {
            _staffRoles = await StaffService.GetStaffRolesAsync(new GetStaffRolesQuery());

            _staffTypes = await StaffService.GetStaffTypesAsync(new GetStaffTypesQuery());
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

        private async Task SaveContactInformation()
        {
            _contactWriter = new ContactWriter(DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, 0l);
        }

        private async Task SaveStaffInformation()
        {
            _staffWriter = new StaffWriter(_contactWriter.Id, 1, 1, false, false, false, string.Empty);
        }

        private async Task OnSubmit()
        {
            await Mediatr.Send(new AddFullStaffEntryCommand(_staffWriter, _contactWriter));
        }
    }
}
