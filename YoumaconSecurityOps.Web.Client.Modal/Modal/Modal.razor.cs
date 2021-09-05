using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using YoumaconSecurityOps.Web.Client.Modal.Core;
using YoumaconSecurityOps.Web.Client.Modal.Core.Configuration;

namespace YoumaconSecurityOps.Web.Client.Modal.Modal
{
    public partial class Modal: ComponentBase
    {
        #region Injected Services
        [CascadingParameter] private IModalService ModalService { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject] private IJSRuntime JsRuntime { get; set; }
        #endregion

        #region Modal Configuration Parameters
        [Parameter] public Boolean? HasDisabledKeyboardClose { get; set; }

        [Parameter] public Boolean? HasHiddenHeader { get; set; }

        [Parameter] public Boolean? HasHiddenCloseButton { get; set; }

        [Parameter] public Boolean? HasStaticBackground { get; set; }

        [Parameter] public ModalPosition? Position { get; set; }

        [Parameter] public String Class { get; set; }
        #endregion

        private readonly ICollection<ModalReference> _modals = new Collection<ModalReference>();

        private readonly ModalOptions _globalModalOptions = new();

        protected override void OnInitialized()
        {
            if (ModalService is null)
            {
                throw new InvalidOperationException($"{GetType()} requires a cascading parameter of type {nameof(IModalService)}");
            }

            ((ModalService) ModalService).OnModalInstanceAdded += Update;

            ((ModalService) ModalService).OnModalCloseRequested += CloseInstance;

            NavigationManager.LocationChanged += CancelModals;

            _globalModalOptions.Class = Class;

            _globalModalOptions.IsBackgroundDisabled = HasStaticBackground;

            _globalModalOptions.IsCloseButtonHidden = HasHiddenCloseButton;

            _globalModalOptions.IsKeyboardAllowedToClose = HasDisabledKeyboardClose;

            _globalModalOptions.IsHeaderHidden = HasHiddenHeader;

            _globalModalOptions.Position = Position;
        }

        internal async Task CancelInstance(Guid id)
        {
           await DismissInstance(id, ModalResult.Cancel());
        }

        internal async void CloseInstance(ModalReference modal, ModalResult result)
        {
           await DismissInstance(modal.Id, result);
        }

        internal async Task CloseInstance(Guid id)
        {
           await DismissInstance(id, ModalResult.Ok<object>(null));
        }
 
        internal async Task DismissInstance(Guid modalId, ModalResult result)
        {
            var reference = _modals.SingleOrDefault(x => x.Id == modalId);

            if (reference is null)
            {
                return;
            }

            await JsRuntime.InvokeVoidAsync("Modal.deactivateFocusTrap", modalId);
            reference.Dismiss(result);
            _modals.Remove(reference);
            StateHasChanged();
        }

        private async void CancelModals(Object sender, LocationChangedEventArgs args)
        {
            foreach (var modalReference in _modals)
            {
                modalReference.Dismiss(ModalResult.Cancel());
            }

            _modals.Clear();

            await InvokeAsync(StateHasChanged);
        }

        private async void Update(ModalReference modalReference)
        {
            await JsRuntime.InvokeVoidAsync("Modal.activateScrollLock");

            _modals.Add(modalReference);

            await InvokeAsync(StateHasChanged);
        }
    }
}
