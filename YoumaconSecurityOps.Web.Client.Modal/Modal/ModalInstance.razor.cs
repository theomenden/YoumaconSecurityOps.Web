using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using YoumaconSecurityOps.Web.Client.Modal.Core;
using YoumaconSecurityOps.Web.Client.Modal.Core.Configuration;

namespace YoumaconSecurityOps.Web.Client.Modal.Modal
{
    public partial class ModalInstance: ComponentBase
    {
        #region Parameters
        [Inject] private IJSRuntime JsRuntime { get; set; }

        [CascadingParameter] private Modal Parent { get; set; }

        [CascadingParameter] private ModalOptions GlobalModalOptions { get; set; }

        [Parameter] public ModalOptions Options { get; set; }

        [Parameter] public string Title { get; set; }

        [Parameter] public RenderFragment BodyContent { get; set; }

        [Parameter] public RenderFragment FooterContent { get; set; }

        [Parameter] public Guid Id { get; set; }
        #endregion

        #region ModalProperties
        private string Position { get; set; }

        private string Class { get; set; }

        private string DialogClass { get; set; }

        private bool IsHeaderHidden { get; set; }

        private bool IsCloseButtonHidden { get; set; }

        private bool IsBackgroundStatic { get; set; }

        private bool IsKeyboardAllowedToClose { get; set; }

        private ElementReference _modalReference;
        #endregion

        protected override void OnInitialized()
        {
            ConfigureInstance();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsRuntime.InvokeVoidAsync("Modal.activateFocusTrap", _modalReference, Id);
            }
        }

        public void SetTitle(string title)
        {
            Title = title;
            StateHasChanged();
        }

        public async Task Close()
        {
           await Close(ModalResult.Ok<object>(null));
        }

        public async Task Close(ModalResult modalResult)
        { 
            await Parent.DismissInstance(Id, modalResult);
        }

        public async Task Cancel()
        {
            await Close(ModalResult.Cancel());
        }

        #region Modal Configuration
        private void ConfigureInstance()
        {
            Position = SetPosition();

            Class = SetClass();

            DialogClass = SetDialogClass();

            IsHeaderHidden = SetIsHeaderHidden();

            IsCloseButtonHidden = SetIsCloseButtonHidden();

            IsBackgroundStatic = SetIsBackgroundStatic();

            IsKeyboardAllowedToClose = SetIsKeyboardAllowedToClose();
        }

        private string SetClass()
        {
            if (!string.IsNullOrWhiteSpace(Options.Class))
            {
                return Options.Class;
            }

            return !string.IsNullOrWhiteSpace(GlobalModalOptions.Class) 
                   ? GlobalModalOptions.Class 
                   : string.Empty;
        }

        private string SetDialogClass()
        {
            if (!string.IsNullOrWhiteSpace(Options.DialogClass))
            {
                return Options.DialogClass;
            }

            if (!string.IsNullOrWhiteSpace(GlobalModalOptions.DialogClass))
            {
                return GlobalModalOptions.DialogClass;
            }

            return string.Empty;
        }

        private bool SetIsBackgroundStatic()
        {
            if (Options.IsBackgroundDisabled.HasValue)
            {
                return Options.IsBackgroundDisabled.Value;
            }

            if (GlobalModalOptions.IsBackgroundDisabled.HasValue)
            {
                return GlobalModalOptions.IsBackgroundDisabled.Value;
            }

            return true;
        }

        private bool SetIsCloseButtonHidden()
        {
            if (Options.IsCloseButtonHidden.HasValue)
            {
                return Options.IsCloseButtonHidden.Value;
            }

            if (GlobalModalOptions.IsCloseButtonHidden.HasValue)
            {
                return GlobalModalOptions.IsCloseButtonHidden.Value;
            }

            return false;
        }

        private bool SetIsHeaderHidden()
        {
            if (Options.IsHeaderHidden.HasValue)
            {
                return Options.IsHeaderHidden.Value;
            }

            if (GlobalModalOptions.IsHeaderHidden.HasValue)
            {
                return GlobalModalOptions.IsHeaderHidden.Value;
            }

            return false;
        }

        private bool SetIsKeyboardAllowedToClose()
        {
            if (Options.IsKeyboardAllowedToClose.HasValue)
            {
                return Options.IsKeyboardAllowedToClose.Value;
            }

            if (GlobalModalOptions.IsKeyboardAllowedToClose.HasValue)
            {
                return GlobalModalOptions.IsKeyboardAllowedToClose.Value;
            }

            return false;
        }

        private string SetPosition()
        {
            ModalPosition position;

            if (Options.Position.HasValue)
            {
                position = Options.Position.Value;
            }

            else if (GlobalModalOptions.Position.HasValue)
            {
                position = GlobalModalOptions.Position.Value;
            }
            else
            {
                position = ModalPosition.Center;
            }

            return position switch
            {
                ModalPosition.Center => "modal-dialog-centered",
                ModalPosition.TopRight => "modal-dialog-top-right",
                ModalPosition.TopLeft => "modal-dialog-top-left",
                ModalPosition.BottomRight => "modal-dialog-bottom-right",
                ModalPosition.BottomLeft => "modal-dialog-bottom-left",
                _ => "modal-dialog-centered"
            };
        }

        private async Task HandleBackgroundClick()
        {
            if (IsBackgroundStatic) return;

            await Parent.CancelInstance(Id);
        }
        #endregion
    }
}
