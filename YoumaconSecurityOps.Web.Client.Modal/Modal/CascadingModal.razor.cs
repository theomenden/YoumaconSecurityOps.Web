using Microsoft.AspNetCore.Components;
using System;
using YoumaconSecurityOps.Web.Client.Modal.Core;
using YoumaconSecurityOps.Web.Client.Modal.Core.Configuration;

namespace YoumaconSecurityOps.Web.Client.Modal.Modal
{
    public partial class CascadingModal: ComponentBase
    {
        [Inject]private  IModalService ModalService { get; set; }

        #region Modal Configuration Options
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool? IsHeaderHidden { get; set; }
        [Parameter] public bool? IsCloseButtonHidden { get; set; }
        [Parameter] public bool? IsBackgroundStatic { get; set; }
        [Parameter] public ModalPosition? Position { get; set; }
        [Parameter] public String Class { get; set; }
        #endregion
    }
}
