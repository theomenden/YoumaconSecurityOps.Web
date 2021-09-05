using System;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Web.Client.Modal.Core.Configuration;

namespace YoumaconSecurityOps.Web.Client.Modal.Core
{
    public interface IModalService
    {
        #region SingleComponentModal
        IModalReference Show<TComponent>() where TComponent : ComponentBase;

        IModalReference Show<TComponent>(string title) where TComponent : ComponentBase;

        IModalReference Show<TComponent>(string title, ModalOptions options) where TComponent : ComponentBase;

        IModalReference Show<TComponent>(string title, ModalParameters parameters) where TComponent : ComponentBase;

        IModalReference Show<TComponent>(string title, ModalOptions options, ModalParameters parameters) where TComponent : ComponentBase;

        IModalReference Show(Type component);

        IModalReference Show(Type component, string title);

        IModalReference Show(Type component, string title, ModalOptions options);

        IModalReference Show(Type component, string title, ModalParameters parameters);

        IModalReference Show(Type component, string title, ModalParameters parameters, ModalOptions options);

        #endregion
    }
}
