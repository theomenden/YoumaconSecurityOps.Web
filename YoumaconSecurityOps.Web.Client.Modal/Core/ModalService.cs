using System;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Web.Client.Modal.Core.Configuration;
using YoumaconSecurityOps.Web.Client.Modal.Modal;

namespace YoumaconSecurityOps.Web.Client.Modal.Core
{
    public class ModalService : IModalService
    {
        internal event Action<ModalReference> OnModalInstanceAdded;

        internal event Action<ModalReference, ModalResult> OnModalCloseRequested;

        #region SingleComponentModal

        public IModalReference Show<T>() where T : ComponentBase
        {
            return Show<T>(string.Empty, new ModalOptions(), new ModalParameters());
        }

        public IModalReference Show<T>(string title) where T : ComponentBase
        {
            return Show<T>(title, new ModalOptions(), new ModalParameters());
        }

        public IModalReference Show<T>(string title, ModalOptions options) where T : ComponentBase
        {
            return Show<T>(title, options, new ModalParameters());
        }

        public IModalReference Show<T>(string title, ModalParameters parameters) where T : ComponentBase
        {
            return Show<T>(title, new ModalOptions(), parameters);
        }

        public IModalReference Show<T>(string title, ModalOptions options, ModalParameters parameters) where T : ComponentBase
        {
            return Show(typeof(T), title, parameters, options);
        }

        public IModalReference Show(Type contentComponent)
        {
            return Show(contentComponent, string.Empty, new ModalParameters(), new ModalOptions());
        }

        public IModalReference Show(Type contentComponent, string title)
        {
            return Show(contentComponent, title, new ModalParameters(), new ModalOptions());
        }

        public IModalReference Show(Type contentComponent, string title, ModalOptions options)
        {
            return Show(contentComponent, title, new ModalParameters(), options);
        }

        public IModalReference Show(Type contentComponent, string title, ModalParameters parameters)
        {
            return Show(contentComponent, title, parameters, new ModalOptions());
        }

        public IModalReference Show(Type contentComponent, string title, ModalParameters parameters, ModalOptions options)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException($"{contentComponent.FullName} must be a blazor component");
            }

            var modalInstanceId = Guid.NewGuid();

            var modalContent = new RenderFragment(builder =>
            {
                var i = 0;

                builder.OpenComponent(i++, contentComponent);

                foreach (var parameter in parameters._parameters)
                {
                    builder.AddAttribute(i++, parameter.Key, parameter.Value);
                }

                builder.CloseComponent();
            });

            var modalInstance = new RenderFragment(builder =>
            {
                builder.OpenComponent<ModalInstance>(0);

                builder.AddAttribute(1, "Options", options);

                builder.AddAttribute(2, "Title", title);

                builder.AddAttribute(3, "BodyContent", modalContent);

                builder.AddAttribute(4, "Id", modalInstanceId);

                builder.CloseComponent();
            });

            var modalReference = new ModalReference(modalInstanceId, modalInstance, this);

            OnModalInstanceAdded?.Invoke(modalReference);

            return modalReference;
        }

        #endregion

        #region CloseModalOperations

        internal void Close(ModalReference modal)
        {
            Close(modal, ModalResult.Ok<object>(null));
        }

        internal void Close(ModalReference modal, ModalResult result)
        {
            OnModalCloseRequested?.Invoke(modal, result);
        }
        #endregion
    }
}
