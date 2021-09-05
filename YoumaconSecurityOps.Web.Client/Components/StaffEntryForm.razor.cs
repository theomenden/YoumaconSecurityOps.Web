using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Web.Client.Components
{
    public partial class StaffEntryForm : ComponentBase
    {
        [Inject] public IStaffRoleAccessor Roles {get; set;}

        [Inject] public IMediator Mediatr { get; set; }

        private IEnumerable<StaffRole> _roleList = new List<StaffRole>(6);

        protected override async Task OnInitializedAsync()
        {
            _roleList = await Roles.GetAll().ToListAsync();
        }

        public Int32 SelectedRoleId { get; set; } = 5;

        public void SelectedRoleChangedHandler(int roleId)
        {
            SelectedRoleId = roleId;
        }

        public async Task OnSubmit()
        {
            await Mediatr.Send(new AddFullStaffEntryCommand(default, default));
        }
    }
}
