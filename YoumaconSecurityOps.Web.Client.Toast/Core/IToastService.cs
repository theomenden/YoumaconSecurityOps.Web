using System;
using Microsoft.AspNetCore.Components;

namespace YoumaconSecurityOps.Web.Client.Toast.Core
{
    public interface IToastService
    {
        event Action<ToastLevel, RenderFragment, string> OnShow;

        void ShowInfo(string message, string heading = "");

        void ShowInfo(RenderFragment message, string heading = "");

        void ShowSuccess(string message, string heading = "");

        void ShowSuccess(RenderFragment message, string heading = "");

        void ShowWarning(string message, string heading = "");

        void ShowWarning(RenderFragment message, string heading = "");

        void ShowError(RenderFragment message, string heading = "");

        void ShowError(string message, string heading);

        void ShowToast(ToastLevel level, string message, string heading = "");

        void ShowToast(ToastLevel level, RenderFragment message, string heading = "");
    }
}
