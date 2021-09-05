using System;
using System.Timers;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Web.Client.Toast.Core;

namespace YoumaconSecurityOps.Web.Client.Toast.Services
{
    public sealed class ToastService : IDisposable, IToastService
    {
        public event Action<ToastLevel, RenderFragment, string> OnShow;

        public event Action OnHide;

        private Timer _countdown;

        public void ShowToast(ToastLevel level, string message, string heading = "")
        {
            ShowToast(level, builder => builder.AddContent(0, message), heading);
        }

        public void ShowToast(ToastLevel level, RenderFragment message, string heading = "")
        {
            OnShow?.Invoke(level, message, heading);
        }

        public void ShowInfo(string message, string heading = "")
        {
            ShowToast(ToastLevel.Info, message, heading);
        }

        public void ShowInfo(RenderFragment message, string heading = "")
        {
            ShowToast(ToastLevel.Info, message, heading);
        }

        public void ShowSuccess(string message, string heading = "")
        {
            ShowToast(ToastLevel.Success, message, heading);
        }

        public void ShowSuccess(RenderFragment message, string heading = "")
        {
            ShowToast(ToastLevel.Success, message, heading);
        }

        public void ShowWarning(string message, string heading = "")
        {
            ShowToast(ToastLevel.Warning, message, heading);
        }

        public void ShowWarning(RenderFragment message, string heading = "")
        {
            ShowToast(ToastLevel.Warning, message, heading);
        }

        public void ShowError(RenderFragment message, string heading = "")
        {
            ShowToast(ToastLevel.Error, message, heading);
        }

        public void ShowError(string message, string heading)
        {
            ShowToast(ToastLevel.Error, message, heading);
        }

        private void StartCountdown()
        {
            SetCountdown();


            if (_countdown.Enabled)
            {
                _countdown.Stop();
                _countdown.Start();
            }
            else
            {
                _countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (!(_countdown is null))
            {
                return;
            }

            _countdown = new Timer(5000);
            _countdown.Elapsed += HideToast;
            _countdown.AutoReset = false;
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            _countdown?.Dispose();
        }
    }
}
